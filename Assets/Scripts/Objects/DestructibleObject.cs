using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    private Animator anim;

    private int counter;

    public int shotsBeforeDestroy;

    public float damage;

    public bool noDeath;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void DamageObject()
    {
        counter++;

        if (counter >= shotsBeforeDestroy)
        {
            if (!noDeath)
            {
                anim.Play("Destroy");
            }
        }
    }

    public void DestroyObject()
    {
        if(!noDeath)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerStats>().LoseHealth(damage);
        }
    }
}
