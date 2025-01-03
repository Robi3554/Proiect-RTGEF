using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : ProjectileScript
{
    [SerializeField]
    private GameObject explosion;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        if (col.CompareTag("Destructable"))
        {
            DestructibleObject destructible = col.gameObject.GetComponent<DestructibleObject>();

            if (destructible != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                destructible.DestroyObject();
                Destroy(gameObject);
            }
        }
    }
}
