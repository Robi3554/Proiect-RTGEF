using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float damage;

    private void Start()
    {
        damage = PlayerStatsManager.Instance.damage * PlayerStatsManager.Instance.projectileExplosionModifier;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
