using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : PlayerBasicWeapon
{
    public LineRenderer lr;

    public float fadeTime;

    public LayerMask enemyMask;

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
        int enemiesDamaged = 0;

        RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.position, firePoint.up, range, enemyMask);

        System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

        lr.SetPosition(0, firePoint.position);

        Vector2 finalPoint = firePoint.position + firePoint.up * range;

        foreach (RaycastHit2D hit in hits)
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                finalPoint = hit.point;

                enemiesDamaged++;

                if (enemiesDamaged >= enemyHit)
                    break;
            }
        }

        lr.SetPosition(1, finalPoint);

        lr.enabled = true;

        yield return new WaitForSeconds(fadeTime);

        lr.enabled = false;
    }
}
