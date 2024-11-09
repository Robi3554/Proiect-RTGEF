using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : PlayerBasicWeapon
{
    protected float projectileSpeed;

    public GameObject projectile;

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

        GameObject shotProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);

        shotProjectile.GetComponent<ProjectileScript>().FireProjectile(CheckDamage(damage), projectileSpeed);
    }

    protected override void GetStats()
    {
        base.GetStats();

        projectileSpeed = PlayerStatsManager.Instance.projectileSpeed;
    }
}
