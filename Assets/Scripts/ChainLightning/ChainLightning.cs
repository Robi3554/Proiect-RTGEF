using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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

    private float lifetime;

    void Start()
    {
        if (amountToChain == 0)
            Destroy(gameObject);

        cc = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();

        startObject = gameObject;

        singleSpawns = 1;
    }

    private void Update()
    {
        lifetime += Time.deltaTime;

        if (lifetime > 0.5f)
        {
            Debug.Log("Chain Lightning exceeded 0.5 seconds, destroying.");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (enemyLayer == (enemyLayer | (1 << col.gameObject.layer)) && !col.GetComponentInChildren<EnemyStruck>())
        {
            if (singleSpawns != 0)
            {
                endObject = col.gameObject;

                amountToChain -= 1;

                Instantiate(chainLightningEffect, col.gameObject.transform.position, Quaternion.identity);

                Instantiate(beenStruck, col.gameObject.transform);

                col.gameObject.GetComponent<Enemy>().TakeDamage(PlayerStatsManager.Instance.damage / 2);

                Debug.Log("Enemy Hit By Lightning!");

                anim.StopPlayback();

                singleSpawns--;

                ps.Play();

                var emitParams = new ParticleSystem.EmitParams();

                emitParams.position = startObject.transform.position;

                ps.Emit(emitParams, 1);

                emitParams.position = endObject.transform.position;

                ps.Emit(emitParams, 1);

                emitParams.position = (startObject.transform.position + endObject.transform.position) / 2;

                Destroy(gameObject, 0.4f);
            }
        }
    }
}
