using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private Transform tr;

    public GameObject echo;

    private float timeBtwSpanws;

    public float startTime;

    internal bool canEcho = false;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if(canEcho)
        {
            if (timeBtwSpanws <= 0)
            {
                Instantiate(echo, transform.position, tr.rotation);
                timeBtwSpanws = startTime;
            }
            else
            {
                timeBtwSpanws -= Time.deltaTime;
            }
        }      
    }
}
