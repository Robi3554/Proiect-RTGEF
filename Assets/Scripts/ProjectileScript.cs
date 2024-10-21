using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private float xStartPos;
    private float yStartPos;

    [Header("Projectile Stats")]
    [SerializeField]
    private float travelDistance;
    private float speed;
    private float damage;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;

        xStartPos = transform.position.x;
        yStartPos = transform.position.y;
    }

    private void FixedUpdate()
    {
        float distanceX = Mathf.Abs(xStartPos - transform.position.x);
        float distanceY = Mathf.Abs(yStartPos - transform.position.y);

        if (distanceX >= travelDistance || distanceY >= travelDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    public void FireProjectile(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }
}
