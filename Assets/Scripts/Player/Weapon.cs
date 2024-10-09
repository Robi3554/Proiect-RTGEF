using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform firePoint;
    private PlayerStats ps;

    public GameObject projectile;
    public LineRenderer lr;

    private float nextFireTime;
    private float damage;

    public float fireRate;
    public float fadeTime;

    private void Awake()
    {
        ps = GetComponentInParent<PlayerStats>();
    }

    void Start()
    {
        firePoint = gameObject.transform;
        damage = ps.damage;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            //Shoot();

            StartCoroutine(Shoot());
        }
    }

    //public void Shoot()
    //{
    //    GameObject shotProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
    //}

    public IEnumerator Shoot()
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
