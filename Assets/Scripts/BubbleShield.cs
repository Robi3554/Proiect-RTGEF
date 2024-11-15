using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D bc;
    private SpriteRenderer sr;

    public float timeBetweenActivations;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        anim.Play("Activate");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            TurnOff();
            StartCoroutine(ReActivate());
            StartCoroutine(GetComponentInParent<PlayerStats>().IFrames());
        }
    }

    private IEnumerator ReActivate()
    {
        yield return new WaitForSeconds(timeBetweenActivations * 60);

        TurnOn();
    }

    private void TurnOff()
    {
        sr.enabled = false;
        anim.Play("Deactivate");
    }

    private void TurnOn()
    {
        sr.enabled = true;
        anim.Play("Activate");
    }
}
