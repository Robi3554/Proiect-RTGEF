using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private float traveDistance;
    private float xStartPosition;

    private int speed = 5;
    private int damage;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        
    }
}
