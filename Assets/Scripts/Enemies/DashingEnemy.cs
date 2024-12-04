using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingEnemy : Enemy
{
    public DashingEnemySO dashingSO;

    private CircleCollider2D cc;

    private float dashingPower;
    private float dashingCooldown;
    private bool isDashing = false;
    private bool canDash = true;

    protected override void Awake()
    {
        base.Awake();

        cc = GetComponent<CircleCollider2D>();
    }

    protected override void Start()
    {
        base.Start();

        dashingPower = dashingSO.dashingPower;
        dashingCooldown = dashingSO.dashingCooldown;
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

            if (!isDashing)
            {
                rb.velocity = direction * speed;

                anim.Play("Moving");

            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            rb.rotation = angle + offsetAngle;
        }
    }

    protected override void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            if (isDashing)
            {
                col.gameObject.GetComponent<PlayerStats>().LoseHealth(damage * 2);
            }
            else
            {
                col.gameObject.GetComponent<PlayerStats>().LoseHealth(damage);
            }            
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && canDash)
        {
            rb.velocity = Vector2.zero;
            isDashing = true;
            anim.Play("Dashing");
        }
    }

    public void SetDashFalse()
    {
        isDashing = false;
    }

    public IEnumerator Dash()
    {
        canDash = false;

        Vector2 directionToPlayer = (target.position - transform.position).normalized;
        rb.velocity = directionToPlayer * dashingPower;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    private void OnDestroy()
    {
        StopCoroutine(Dash());
    }
}
