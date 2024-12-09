using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingEnemy : Enemy
{
    public DashingEnemySO dashingSO;

    private EchoEffect ee;

    public float dashPrepTime;

    private float dashingPower;
    private float dashingCooldown;
    private float dashTime;
    private bool isDashing = false;
    private bool canDash = true;

    protected override void Awake()
    {
        base.Awake();

        ee = GetComponent<EchoEffect>();
    }

    protected override void Start()
    {
        base.Start();

        dashingPower = dashingSO.dashingPower;
        dashingCooldown = dashingSO.dashingCooldown;
        dashTime = dashingSO.dashTime;
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

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                rb.rotation = angle + offsetAngle;
            }
            else
            {
                rb.rotation = rb.rotation;
            }
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

    public IEnumerator Dash()
    {
        canDash = false;

        yield return new WaitForSeconds(dashPrepTime);

        Vector2 directionToPlayer = (target.position - transform.position).normalized;
        rb.velocity = directionToPlayer * dashingPower;
        ee.canEcho = true;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        ee.canEcho = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    private void OnDestroy()
    {
        StopCoroutine(Dash());
    }
}
