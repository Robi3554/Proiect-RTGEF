using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public GameObject projectile;

    public Transform targetSpot;
    public Transform mainShip;

    public Transform firePoint;

    private Rigidbody2D rb;

    public delegate void MinionDestroyedHandler();
    public static event MinionDestroyedHandler OnMinionDestroyed;

    [Header("For Shooting")]
    private Transform targetEnemy;
    protected float nextFireTime;
    private float damage;
    private float fireRate;
    private float projectileSpeed;
    private bool isAtEnemy = false;

    [Header("Minion Stats")]
    private float moveSpeed;
    private float stoppingDistance = 0.1f;
    public float followSpeed;
    public float minionModifier;
    public float rayCastRange;

    private bool isAtPoint = false;
    private bool isFollowingEnemy = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged += GetStats;
        }
    }

    private void FixedUpdate()
    {
        if (isFollowingEnemy && targetEnemy != null)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Enemy");

            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, (targetEnemy.position - firePoint.position).normalized, rayCastRange, layerMask);

            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                isAtEnemy = true;
                rb.velocity = Vector2.zero;

                if (Time.time >= nextFireTime)
                {
                    nextFireTime = Time.time + (1 / fireRate);

                    Shoot();
                }
            }
            else
            {
                isAtEnemy = false;
            }

            if (!isAtEnemy)
            {
                FollowTarget(targetEnemy);
            }

            RotateTowardsTargetEnemy();
        }
        else if (targetSpot != null)
        {
            if (!isAtPoint)
            {
                MoveTowardsTargetSpot();
            }
            else
            {
                FollowTarget(targetSpot);
            }

            RotateTowardsMainShip();
        }

        if (Vector2.Distance(transform.position, targetSpot.position) > stoppingDistance)
        {
            isAtPoint = false;
        }
    }

    private void Shoot()
    {
        GameObject shotProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);

        shotProjectile.GetComponent<ProjectileScript>().FireProjectile(damage, projectileSpeed);
    }

    private void GetStats()
    {
        moveSpeed = PlayerStatsManager.Instance.moveSpeed * minionModifier;
        damage = PlayerStatsManager.Instance.damage * minionModifier;
        fireRate = PlayerStatsManager.Instance.fireRate * minionModifier;
        projectileSpeed = PlayerStatsManager.Instance.projectileSpeed * minionModifier;
    }

    private void MoveTowardsTargetSpot()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetSpot.position, moveSpeed);
        rb.MovePosition(newPosition);

        if (Vector2.Distance(transform.position, targetSpot.position) < stoppingDistance)
        {
            rb.MovePosition(targetSpot.position);
            isAtPoint = true;
        }
    }

    private void FollowTarget(Transform target)
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target.position, followSpeed);

        rb.MovePosition(newPosition);
    }

    private void RotateTowardsTargetEnemy()
    {
        Vector2 directionToEnemy = (targetEnemy.position - transform.position).normalized;

        float targetAngle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg -90;
        float currentAngle = rb.rotation;

        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

        float maxAngularSpeed = 500f;

        rb.angularVelocity = Mathf.Clamp(angleDifference * 8f, -maxAngularSpeed, maxAngularSpeed);

        if (Mathf.Abs(angleDifference) < 1f)
        {
            rb.angularVelocity = 0f;
        }
    }

    private void RotateTowardsMainShip()
    {
        float targetAngle = mainShip.eulerAngles.z;
        float currentAngle = rb.rotation;

        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

        float maxAngularSpeed = 500f;

        rb.angularVelocity = Mathf.Clamp(angleDifference * 6f, -maxAngularSpeed, maxAngularSpeed);

        if (Mathf.Abs(angleDifference) < 1f)
        {
            rb.angularVelocity = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            isFollowingEnemy = true;

            targetEnemy = col.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            isFollowingEnemy = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (firePoint.position - transform.position).normalized * rayCastRange);
    }

    private void OnDestroy()
    {
        if (OnMinionDestroyed != null)
        {
            OnMinionDestroyed.Invoke();
        }

        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged -= GetStats;
        }
    }

    private void OnDisable()
    {
        OnMinionDestroyed -= OnMinionDestroyed;
    }
}
