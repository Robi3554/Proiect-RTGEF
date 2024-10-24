using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    protected Transform firePoint;

    protected float nextFireTime;
    protected float damage;
    protected float fireRate;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        firePoint = gameObject.transform;
        damage = PlayerStatsManager.Instance.damage;
        fireRate = PlayerStatsManager.Instance.fireRate;
    }

    protected virtual void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Shoot();       
        }
    }

    protected virtual void Shoot()
    {

    }
}
