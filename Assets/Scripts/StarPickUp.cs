using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickUp : MonoBehaviour
{
    public int expToAdd;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddExp(expToAdd);
            Destroy(gameObject);
        }
    }
}
