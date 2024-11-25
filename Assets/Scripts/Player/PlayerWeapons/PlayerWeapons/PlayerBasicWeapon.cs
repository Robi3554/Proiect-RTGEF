using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicWeapon : BasicWeapon
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

    protected override void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !GameManager.Instance.isPaused)
        {
            nextFireTime = Time.time + (1 / fireRate);

            Shoot();
        }
    }

    protected override void GetStats()
    {
        base.GetStats();
    }
}
