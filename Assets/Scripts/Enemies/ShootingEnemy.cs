using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField]
    private ShootingEnemySO shootingSO;

    private CircleCollider2D cc;

    public Transform firePoint;
    public GameObject projectile;

    [Header("For Shooting")]
    private float nextFireTime;
    private float fireRate;
    private float range;
    private float projectileSpeed;

    private bool isShooting = false;

    protected override void Awake()
    {
        base.Awake();

        cc = GetComponent<CircleCollider2D>();
    }

    protected override void Start()
    {
        base.Start();

        fireRate = shootingSO.fireRate;
        range = shootingSO.range;
        projectileSpeed = shootingSO.projectileSpeed;
    }

    protected override void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) >= despawnDistance)
            {
                ReturnEnemy();
            }

            Vector2 direction = (target.position - transform.position).normalized;

            if (!isShooting)
            {
                rb.velocity = direction * speed;

                anim.Play("Moving");

            }
            else
            {
                rb.velocity = Vector2.zero;
                anim.Play("Idle");
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            rb.rotation = angle + offsetAngle;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isShooting = true;

            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + (1f / fireRate);
                Shoot();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isShooting = false;
    }

    private void Shoot()
    {
        GameObject shotProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 180));

        shotProjectile.GetComponent<EnemyProjectile>().FireProjectile(damage, projectileSpeed, range);
    }
}
