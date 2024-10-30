using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : PlayerBasicWeapon
{
    public LineRenderer lr;

    public float fadeTime;

    protected override void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + (1 / fireRate);

            StartCoroutine(Shoot());
        }
    }

    protected new IEnumerator Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.up);

        if (hit)
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hit.point);
        }
        else
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, firePoint.position + firePoint.up * 100);
        }

        lr.enabled = true;

        yield return new WaitForSeconds(fadeTime);

        lr.enabled = false;
    }
}
