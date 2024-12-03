using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Transform target;
    protected Rigidbody2D rb;
    protected Animator anim;

    [SerializeField]
    private EnemySO enemySO;

    protected float health;
    protected float damage;
    protected float speed;
    public float offsetAngle = 90f;

    public int scoreToIncrease;

    public float despawnDistance;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        health = enemySO.health;
        damage = enemySO.damage;
        speed = enemySO.speed;
    }

    protected virtual void FixedUpdate()
    {
        if (target != null)
        {
            if(Vector2.Distance(transform.position, target.position) >= despawnDistance)
            {
                ReturnEnemy();
            }

            Vector2 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * speed;

            if(rb.velocity != Vector2.zero)
            {
                anim.Play("Moving");
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            rb.rotation = angle + offsetAngle;

            //Debug.Log($"Speed: {speed}, Velocity: {rb.velocity.magnitude}");
        }
    }

    public void RestartLoop()
    {
        anim.Play("Moving", 0, 0.25f);
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

    protected void ReturnEnemy()
    {
        EnemySpawner es = EnemySpawner.Instance;

        Vector2 targetPos = target.position + es.relativeSpawnPositions[Random.Range(0, es.relativeSpawnPositions.Count)].position; 

        rb.position = targetPos;
    }

    protected void OnCollisionStay2D(Collision2D col)
    {
        if(col.transform.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerStats>().LoseHealth(damage);
        }
    }

    protected void Die()
    {
        GameManager.Instance.IncreaseScore(scoreToIncrease);
        EnemySpawner.Instance.OnEnemyKilled();
    }
}
