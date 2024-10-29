using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartWeapon : MonoBehaviour
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
        damage = PlayerStatsManager.Instance.damage;
        fireRate = PlayerStatsManager.Instance.fireRate;
    }

    protected virtual void Shoot()
    {
        Debug.Log("SHOOT!");
    }

    protected virtual void OnParentStay2D(Collider2D col)
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Shoot();
        }
    }
}
