using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    private CircleCollider2D cc;
    private Animator anim;
    private ParticleSystem ps;

    public GameObject chainLightningEffect;

    public GameObject beenStruck;

    private GameObject startObject;
    public GameObject endObject;

    public LayerMask enemyLayer;

    public float damage;
    public int amountToChain;

    private int singleSpawns;

    void Start()
    {
        if(amountToChain == 0) 
            Destroy(gameObject);

        cc = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();

        startObject = gameObject;

        singleSpawns = 1;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(enemyLayer == (enemyLayer | (1 << col.gameObject.layer)) && !col.GetComponentInChildren<EnemyStruck>())
        {
            if(singleSpawns != 0)
            {
                endObject = col.gameObject;

                amountToChain -= 1;

                Instantiate(chainLightningEffect, transform.position, Quaternion.identity);

                Instantiate(beenStruck, col.gameObject.transform);

                Debug.Log("Enemy Hit By Lightning!");

                anim.StopPlayback();

                //col.enabled = false;

                singleSpawns--;

                ps.Play();

                var emitParams = new ParticleSystem.EmitParams();

                emitParams.position = startObject.transform.position;

                ps.Emit(emitParams, 1);

                emitParams.position = endObject.transform.position;

                ps.Emit(emitParams, 1);

                Destroy(gameObject);
            }
        } 
    }
}
