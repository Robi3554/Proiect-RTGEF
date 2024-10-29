using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartProjectileWeapon : BodyPartWeapon
{
    public GameObject projectile;

    protected float projectileSpeed;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        projectileSpeed = PlayerStatsManager.Instance.projectileSpeed;
    }

    protected override void Shoot()
    {
        GameObject shotProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);

        shotProjectile.GetComponent<ProjectileScript>().FireProjectile(damage, projectileSpeed);
    }

    protected override void OnParentStay2D(Collider2D col)
    {
        base.OnParentStay2D(col);   
    }
}
