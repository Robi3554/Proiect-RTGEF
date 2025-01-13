using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickUp : MonoBehaviour
{
    public float expToAdd;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddExp(Mathf.Round(expToAdd * GameManager.Instance.enemyStatMultiplier));
            Debug.Log(Mathf.Round(expToAdd * GameManager.Instance.enemyStatMultiplier));
            Destroy(gameObject);
        }
    }
}
