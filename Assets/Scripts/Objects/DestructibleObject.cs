using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    private Animator anim;

    private bool noAnim = false;

    private int counter;

    public int shotsBeforeDestroy;

    public float damage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (shotsBeforeDestroy <= 0)
        {
            Debug.LogError("ShotsBeforeDestroy set to and invalid value!");
        }

        if(anim == null)
        {
            noAnim = true;
        }
    }

    public void DamageObject()
    {
        counter++;

        if (counter >= shotsBeforeDestroy)
        {
            if (!noAnim)
            {
                anim.Play("Destroy");
            }
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerStats>().LoseHealth(damage);
        }
    }
}
