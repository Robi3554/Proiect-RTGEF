using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartProjectileWeapon : BodyPartWeapon
{
    private ProjectilePlayerStats pps;

    public GameObject projectile;

    protected override void Awake()
    {
        base.Awake();

        pps = GetComponentInParent<ProjectilePlayerStats>();
    }

    protected override void Shoot()
    {
        base.Shoot();

        GameObject shotProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);

        shotProjectile.GetComponent<ProjectileScript>().FireProjectile(ps.damage, pps.projectileSpeed);
    }
}
