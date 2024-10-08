using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private float xStartPos;
    private float yStartPos;

    [Header("Projectile Stats")]
    private float travelDistance = 12f;
    private int speed = 10;
    private int damage = 10;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;

        xStartPos = transform.position.x;
        yStartPos = transform.position.y;
    }

    void Update()
    {
        
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
}
