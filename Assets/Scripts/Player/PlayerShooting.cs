using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Properties")]
    [SerializeField] Bullet bulletPrefab;

    [Header("Raycast Properties")]
    [SerializeField] LayerMask raycastLayers;
    [SerializeField] float raycastDistance = 100;
    [SerializeField] Transform shootPoint;

    ObjectPool<Bullet> bulletPool;

    void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool);    
    }

    public void Shoot()
    {
        Vector3 direction = GetShootDirection();
        var bullet = bulletPool.Get();
        bullet.transform.position = shootPoint.position;
        bullet.SetDirection(direction);
    }

    Vector3 GetShootDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        var hitPoint = ray.GetPoint(raycastDistance);
        if (Physics.Raycast(ray, out hit, raycastDistance, raycastLayers))
            hitPoint = hit.point;
    
        return (hitPoint - shootPoint.position).normalized;
    }

    // Pooling methods
    Bullet CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.SetPool(bulletPool);
        return bullet;
    }

    void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
