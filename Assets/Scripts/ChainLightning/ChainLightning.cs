using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    private CircleCollider2D cc;
    private Animator anim;

    public GameObject chainLightningEffect;
    public GameObject beenStruck;

    private GameObject startObject;
    public GameObject endObject;

    public GameObject travelingSprite;

    public LayerMask enemyLayer;

    public float damage;
    public int amountToChain;

    private int singleSpawns;
    private float lifetime;

    private LineRenderer lineRenderer;

    void Start()
    {
        if (amountToChain == 0)
            Destroy(gameObject);

        cc = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();

        startObject = gameObject;
        singleSpawns = 1;

        lineRenderer.enabled = false;
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

                // Create effects at the target location
                Instantiate(chainLightningEffect, col.gameObject.transform.position, Quaternion.identity);
                Instantiate(beenStruck, col.gameObject.transform);

                Debug.Log("Enemy Hit By Lightning!");

                anim.StopPlayback();
                singleSpawns--;

                // Enable the line renderer
                lineRenderer.enabled = true;

                // Set start and end positions of the line renderer
                lineRenderer.SetPosition(0, startObject.transform.position);
                lineRenderer.SetPosition(1, endObject.transform.position);

                // Calculate direction and angle between start and end
                Vector3 direction = endObject.transform.position - startObject.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Calculate distance between the two points
                float distance = direction.magnitude;

                // Create and position the traveling sprite at the midpoint
                GameObject spriteInstance = Instantiate(travelingSprite, startObject.transform.position, Quaternion.identity);

                // Stretch the sprite to cover the full distance
                spriteInstance.transform.localScale = new Vector3(distance, spriteInstance.transform.localScale.y, spriteInstance.transform.localScale.z);

                // Rotate the sprite to face the line's direction
                spriteInstance.transform.rotation = Quaternion.Euler(0, 0, angle + 90); // Ensure it follows the line direction

                // Destroy the sprite after a short delay
                Destroy(spriteInstance, 0.4f);
                Destroy(gameObject, 0.4f);
            }
        }
    }
}
