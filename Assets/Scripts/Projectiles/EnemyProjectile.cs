using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : ProjectileScript
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();

            if (player != null)
            {
               player.LoseHealth(damage);
               Destroy(gameObject);
            }
        }
    }

    public void FireProjectile(float damage, float speed, float range)
    {
        this.damage = damage;
        this.speed = speed;
        this.range = range;
    }
}
