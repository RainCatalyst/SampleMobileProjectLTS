using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Header("Basic Properties")]
    [SerializeField] int damage = 1;
    [SerializeField] float speed = 30f;
    [SerializeField] float selfDestructTime = 2f;
    [SerializeField] GameObject trailPrefab;

    [Header("Explosion Properties")]
    [SerializeField] float explosionSize = 0.75f;
    [SerializeField] float explosionForce = 0.25f;
    [SerializeField] GameObject impactPrefab;

    Rigidbody rb;
    GameObject trail;
    ObjectPool<Bullet> pool;
    Coroutine killCoroutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        killCoroutine = StartCoroutine(DelayedKill(selfDestructTime));
        CreateTrail();
    }

    void OnDisable()
    {
        StopCoroutine(killCoroutine);
    }

    public void SetPool(ObjectPool<Bullet> pool) => this.pool = pool;

    public void SetDirection(Vector3 direction) => rb.velocity = direction * speed;

    void OnTriggerEnter(Collider other)
    {
        Health health;
        if (other.gameObject.TryGetComponent(out health)) {
            health.DealDamage(damage);
            Instantiate(impactPrefab, transform.position, Quaternion.identity);
        }
            
        CharacterRagdoll ragdoll;
        if (other.gameObject.TryGetComponent(out ragdoll))
            ragdoll.AddImpact(rb.velocity * explosionForce, transform.position, explosionSize);
        
        Kill();
    }

    void Kill()
    {
        RemoveTrail();
        pool.Release(this);
    }

    void CreateTrail()
    {
        if (trail != null)
            return;
        trail = Instantiate(trailPrefab, transform);
    }
    
    void RemoveTrail()
    {
        trail.transform.SetParent(null);
        trail = null;
    }

    IEnumerator DelayedKill(float time)
    {
        yield return new WaitForSeconds(time);
        Kill();
    }
}
