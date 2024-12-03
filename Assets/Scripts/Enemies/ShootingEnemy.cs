using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField]
    private ShootingEnemySO shootingSO;

    private Transform player;

    public Transform firePoint;
    public GameObject Projectile;

    [Header("For Shooting")]
    private float nextFireTime;
    private float fireRate;
    private float range;
    public float visionRange;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        fireRate = shootingSO.fireRate;
        range = shootingSO.range;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        int layerMask = 1 << LayerMask.NameToLayer("Player");

        //RaycastHit2D hit = Physics2D.Raycast(firePoint.position, (player.position - firePoint.position).normalized, visionRange, layerMask);

        //if (hit.collider != null && hit.collider.CompareTag("Player"))
        //{
        //    rb.velocity = Vector2.zero;

        //    if (Time.time >= nextFireTime)
        //    {
        //        nextFireTime = Time.time + (1 / fireRate);

        //        //Shoot();
        //    }
        //}
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (firePoint.position - transform.position).normalized * visionRange);
    }
}
