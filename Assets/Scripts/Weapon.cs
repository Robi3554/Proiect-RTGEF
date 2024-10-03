using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform firePoint;

    public GameObject projectile;

    private float nextFireTime;

    public float fireRate;

    void Start()
    {
        firePoint = gameObject.transform;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject shotProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
    }
}
