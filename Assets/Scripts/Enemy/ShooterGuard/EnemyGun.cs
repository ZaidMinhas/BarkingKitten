using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    
    [SerializeField] Bullet bulletPrefab;
    
    [SerializeField] private float fireRate;
    [SerializeField] private int bulletCount;
    private float nextFireTime;
    
    
    public void Shoot()
    {
        
        if (Time.time >= nextFireTime)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            nextFireTime = Time.time + fireRate;
            
        }        
    }
}
