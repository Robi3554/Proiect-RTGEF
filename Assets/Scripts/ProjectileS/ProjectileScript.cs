using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected float xStartPos;
    protected float yStartPos;

    [Header("Projectile Stats")]
    protected float range;
    protected float speed;
    protected float damage;
    protected int enemyHit;

    private int enemiesDamaged;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;

        xStartPos = transform.position.x;
        yStartPos = transform.position.y;
    }

    protected virtual void FixedUpdate()
    {
        float distanceX = Mathf.Abs(xStartPos - transform.position.x);
        float distanceY = Mathf.Abs(yStartPos - transform.position.y);

        if (distanceX >= range || distanceY >= range)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                enemiesDamaged++;

                if (enemiesDamaged >= enemyHit)
                {
                    Destroy(gameObject);
                }
            }
        }
        if (col.CompareTag("Destructable"))
        {
            DestructibleObject destructible = col.gameObject.GetComponent<DestructibleObject>();

            if(destructible != null)
            {
                destructible.DamageObject();
                Destroy(gameObject);
            }
        }
    }

    public void FireProjectile(float damage, float speed,float range ,int enemyHit)
    {
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.enemyHit = enemyHit;
    }
}
