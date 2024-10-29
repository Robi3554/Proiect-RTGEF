using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Transform targetSpot;
    public Transform mainShip;

    public Transform firePoint;

    public CircleCollider2D cc;

    private Rigidbody2D rb;

    public delegate void MinionDestroyedHandler();
    public static event MinionDestroyedHandler OnMinionDestroyed;

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

    private void Update()
    {
        moveSpeed = PlayerStatsManager.Instance.moveSpeed * minionModifier;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (firePoint.position - transform.position).normalized, rayCastRange);
    }

    private void FixedUpdate()
    {
        if (targetSpot != null && !isFollowingEnemy)
        {
            if (!isAtPoint)
            {
                MoveTowardsTargetSpot();
            }
            else
            {
                FollowTargetSpot();
            }

            RotateTowardsMainShip();
        }
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

    private void FollowTargetSpot()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetSpot.position, followSpeed);

        rb.MovePosition(newPosition);
    }

    private void MoveTowardsTargetEnemy()
    {

    }

    private void RotateTowardsMainShip()
    {
        float targetAngle = mainShip.eulerAngles.z;
        float currentAngle = rb.rotation;

        // Calculate the shortest direction to rotate
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
        float maxRotationSpeed = 200f * Time.fixedDeltaTime; // Ensure it's frame-rate independent

        // Determine the rotation step
        float rotationStep = Mathf.Clamp(angleDifference, -maxRotationSpeed, maxRotationSpeed);

        // Apply the rotation
        rb.rotation = currentAngle + rotationStep;

        // Optional: Snap directly to the target angle if very close
        if (Mathf.Abs(angleDifference) < 0.1f)
        {
            rb.rotation = targetAngle;
        }
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            isFollowingEnemy = true;
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
    }

    private void OnDisable()
    {
        OnMinionDestroyed -= OnMinionDestroyed;
    }
}
