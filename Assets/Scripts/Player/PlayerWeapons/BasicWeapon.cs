using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    protected Transform firePoint;
    protected PlayerStats ps;

    protected float nextFireTime;
    protected float damage;

    public float fireRate;

    protected virtual void Awake()
    {
        ps = GetComponentInParent<PlayerStats>();
    }

    protected virtual void Start()
    {
        firePoint = gameObject.transform;
        damage = ps.damage;
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
