using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public Transform firePoint;

    protected float nextFireTime;
    protected float damage;
    protected float fireRate;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        firePoint = gameObject.transform;
        GetStats();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Shoot()
    {

    }

    protected virtual void GetStats()
    {
        damage = PlayerStatsManager.Instance.damage;
        fireRate = PlayerStatsManager.Instance.fireRate;
    }
}
