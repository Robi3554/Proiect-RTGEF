using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : ProjectileScript
{
    public GameObject beenStruck;

    public GameObject chainLightningEffect;

    public int amountToChain;

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
                chainLightningEffect.GetComponent<ChainLightning>().amountToChain = amountToChain;
                Instantiate(chainLightningEffect, col.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
        if (col.CompareTag("Destructable"))
        {
            DestructibleObject destructible = col.gameObject.GetComponent<DestructibleObject>();

            if (destructible != null)
            {
                destructible.DamageObject();
                Destroy(gameObject);
                Instantiate(beenStruck, col.gameObject.transform);
                chainLightningEffect.GetComponent<ChainLightning>().damage = damage;
                chainLightningEffect.GetComponent<ChainLightning>().amountToChain = amountToChain;
                Instantiate(chainLightningEffect, col.transform.position, Quaternion.identity);
            }
        }
    }

    protected override void Start()
    {
        base.Start();
    }
}
