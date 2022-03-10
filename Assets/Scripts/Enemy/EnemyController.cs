using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health), typeof(CharacterRagdoll))]
public class EnemyController : MonoBehaviour
{
    public UnityAction<EnemyController> OnKilled;
    [HideInInspector] public Health health;
    CharacterRagdoll ragdoll;

    void Awake() {
        health = GetComponent<Health>();
        ragdoll = GetComponent<CharacterRagdoll>();
    }

    void OnEnable()
    {
        health.OnDamageReceived += OnDamageReceived;    
        health.OnDeath += OnDeath;    
    }

    void OnDisable()
    {
        health.OnDamageReceived -= OnDamageReceived;
        health.OnDeath -= OnDeath;
    }

    void OnDamageReceived(int damage)
    {
        // Damage effects
    }

    void OnDeath()
    {
        // Spawn effects etc
        OnKilled?.Invoke(this);
        ragdoll.SetRagdoll(true);
        Destroy(gameObject, 5f);
    }
}
