using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PickUp"))
        {
            Attraction attracable = col.GetComponent<Attraction>();

            if (attracable != null)
            {
                attracable.target = transform;
            }
        }
    }
}
