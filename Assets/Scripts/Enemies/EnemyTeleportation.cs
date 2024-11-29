using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleportation : MonoBehaviour
{
    public float maxDistance = 30f; 
    public float teleportDistance = 20f;
    public float sideOffset = 5f;

    private Transform player; 
    private List<Transform> enemies = new List<Transform>();

    private void Start()
    {
        UpdatePlayerReference();
    }

    void Update()
    {
        if (player == null)
            return;

        UpdateEnemyList();

        Vector3 playerMovementDirection = player.forward;

        foreach (Transform enemy in enemies)
        {
            CheckAndTeleportEnemy(enemy, playerMovementDirection);
        }
    }

    void UpdatePlayerReference()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void UpdateEnemyList()
    {
        enemies.Clear();

        foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemyObj.activeInHierarchy)
            {
                enemies.Add(enemyObj.transform);
            }
        }
    }

    void CheckAndTeleportEnemy(Transform enemy, Vector3 playerMovementDirection)
    {

        float distanceToPlayer = Vector3.Distance(enemy.position, player.position);

        if (distanceToPlayer > maxDistance)
        {
            // Calculate new position
            Vector3 teleportDirection = playerMovementDirection.normalized;
            Vector3 perpendicularOffset = Vector3.Cross(teleportDirection, Vector3.up).normalized * Random.Range(-sideOffset, sideOffset);

            Vector3 teleportPosition = player.position + (teleportDirection * teleportDistance) + perpendicularOffset;

            // Get the Rigidbody2D
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Use MovePosition to set the new position
                rb.MovePosition(teleportPosition);
            }

            Debug.Log($"Enemy {enemy.name} teleported to {teleportPosition} with velocity reset.");
        }
    }
}
