using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    public Transform target;

    public float speed;

    void FixedUpdate()
    {
        if (target)
        {
            Vector2 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed);
        }   
    }
}
