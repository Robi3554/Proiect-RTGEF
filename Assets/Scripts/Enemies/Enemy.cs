using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public float despawnDistance;

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
            if(Vector2.Distance(transform.position, target.position) >= despawnDistance)
            {
                ReturnEnemy();
            }

            Vector2 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            rb.rotation = angle + offsetAngle;

            //Debug.Log($"Speed: {speed}, Velocity: {rb.velocity.magnitude}");
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

    private void ReturnEnemy()
    {
        EnemySpawner es = EnemySpawner.Instance;

        Vector2 targetPos = target.position + es.relativeSpawnPositions[Random.Range(0, es.relativeSpawnPositions.Count)].position; 

        rb.position = targetPos;
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
        EnemySpawner.Instance.OnEnemyKilled();
        Instantiate(star, transform.position, Quaternion.identity);
    }
}
