using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartWeapon : BasicWeapon
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Shoot()
    {
        base.Shoot();
    }

    protected virtual void OnParentStay2D(Collider2D col)
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + (1 / fireRate);

            Shoot();
        }
    }
}
