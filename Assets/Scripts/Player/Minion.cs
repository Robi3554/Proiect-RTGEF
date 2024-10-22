using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Transform targetSpot;

    public float speed = 2f;

    private Rigidbody2D rb;

    private bool isAtTarget = false;

    private float stoppingDistance = 0.1f;

    public delegate void MinionDestroyedHandler();
    public static event MinionDestroyedHandler OnMinionDestroyed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (targetSpot != null)
        {
            if (!isAtTarget)
            {
                MoveTowardsTargetSpot();
            }
            else
            {
                FollowTargetSpot();
            }
        }
    }

    private void MoveTowardsTargetSpot()
    {
        Vector2 direction = (targetSpot.position - transform.position).normalized;

        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetSpot.position, speed);
        rb.MovePosition(newPosition);

        if (Vector2.Distance(transform.position, targetSpot.position) < stoppingDistance)
        {
            rb.MovePosition(targetSpot.position);
            isAtTarget = true;
        }
    }

    private void FollowTargetSpot()
    {
        Vector2 direction = (targetSpot.position - transform.position).normalized;

        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetSpot.position, speed);

        rb.MovePosition(newPosition);
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
