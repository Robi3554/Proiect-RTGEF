using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;

    public GameObject star;

    public float health;
    public float damage;
    public float speed;
    public float offsetAngle = 90f;

    public int scoreToIncrease;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            rb.rotation = angle + offsetAngle;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.transform.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerStats>().LoseHealth(damage);
        }
    }

    private void Die()
    {
        GameManager.Instance.IncreaseScore(scoreToIncrease);
        Instantiate(star, transform.position, Quaternion.identity);
    }
}
