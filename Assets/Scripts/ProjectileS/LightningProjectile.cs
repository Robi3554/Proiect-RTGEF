using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : ProjectileScript
{
    public GameObject beenStruck;

    public GameObject chainLightningEffect;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Instantiate(beenStruck, col.gameObject.transform);
                chainLightningEffect.GetComponent<ChainLightning>().damage = damage;
                chainLightningEffect.GetComponent<ChainLightning>().amountToChain = 5;
                Instantiate(chainLightningEffect, col.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
    }
}
