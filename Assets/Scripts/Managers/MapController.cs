using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> chunks;
    public GameObject currentChunk;

    public LayerMask spaceMask;

    public float checkerRadius;

    private PlayerMovement pm;
    private GameObject player;

    private Vector3 noTerrainPosition;

    [Header("Optimization")]
    public GameObject map;
    public List<GameObject> spawnedChunks = new List<GameObject>();
    public HashSet<Vector3> spawnedPositions = new HashSet<Vector3>();
    private GameObject lastChunk;
    public float maxChunkDistance;
    private float optimizerCooldown;
    public float optimizerCooldownDuration;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimization();
    }

    private void ChunkChecker()
    {
        if (!currentChunk) return;

        Vector3 directionPosition = Vector3.zero;

        switch (pm.currentDirection)
        {
            case Direction.Right:
                directionPosition = currentChunk.transform.Find("Right").position;
                break;
            case Direction.Up_Right:
                directionPosition = currentChunk.transform.Find("Up_Right").position;
                break;
            case Direction.Up:
                directionPosition = currentChunk.transform.Find("Up").position;
                break;
            case Direction.Up_Left:
                directionPosition = currentChunk.transform.Find("Up_Left").position;
                break;
            case Direction.Left:
                directionPosition = currentChunk.transform.Find("Left").position;
                break;
            case Direction.Down_Left:
                directionPosition = currentChunk.transform.Find("Down_Left").position;
                break;
            case Direction.Down:
                directionPosition = currentChunk.transform.Find("Down").position;
                break;
            case Direction.Down_Right:
                directionPosition = currentChunk.transform.Find("Down_Right").position;
                break;
        }

        if (!Physics2D.OverlapCircle(directionPosition, checkerRadius, spaceMask) && !spawnedPositions.Contains(directionPosition))
        {
            noTerrainPosition = directionPosition;
            SpawnChunk();
        }

        Vector3 adjacentPosition1 = Vector3.zero;
        Vector3 adjacentPosition2 = Vector3.zero;

        switch (pm.currentDirection)
        {
            case Direction.Right:
                adjacentPosition1 = currentChunk.transform.Find("Up").position;
                adjacentPosition2 = currentChunk.transform.Find("Down").position;
                break;
            case Direction.Up_Right:
                adjacentPosition1 = currentChunk.transform.Find("Up").position;
                adjacentPosition2 = currentChunk.transform.Find("Right").position;
                break;
            case Direction.Up:
                adjacentPosition1 = currentChunk.transform.Find("Up_Left").position;
                adjacentPosition2 = currentChunk.transform.Find("Up_Right").position;
                break;
            case Direction.Up_Left:
                adjacentPosition1 = currentChunk.transform.Find("Up").position;
                adjacentPosition2 = currentChunk.transform.Find("Left").position;
                break;
            case Direction.Left:
                adjacentPosition1 = currentChunk.transform.Find("Up_Left").position;
                adjacentPosition2 = currentChunk.transform.Find("Down_Left").position;
                break;
            case Direction.Down_Left:
                adjacentPosition1 = currentChunk.transform.Find("Down").position;
                adjacentPosition2 = currentChunk.transform.Find("Left").position;
                break;
            case Direction.Down:
                adjacentPosition1 = currentChunk.transform.Find("Down_Left").position;
                adjacentPosition2 = currentChunk.transform.Find("Down_Right").position;
                break;
            case Direction.Down_Right:
                adjacentPosition1 = currentChunk.transform.Find("Down").position;
                adjacentPosition2 = currentChunk.transform.Find("Right").position;
                break;
        }

        if (!Physics2D.OverlapCircle(adjacentPosition1, checkerRadius, spaceMask) && !spawnedPositions.Contains(adjacentPosition1))
        {
            noTerrainPosition = adjacentPosition1;
            SpawnChunk();
        }

        if (!Physics2D.OverlapCircle(adjacentPosition2, checkerRadius, spaceMask) && !spawnedPositions.Contains(adjacentPosition2))
        {
            noTerrainPosition = adjacentPosition2;
            SpawnChunk();
        }
    }


    private void SpawnChunk()
    {
        int rand = Random.Range(0, chunks.Count);
        lastChunk = Instantiate(chunks[rand], noTerrainPosition, Quaternion.identity);

        lastChunk.transform.SetParent(map.transform);

        spawnedChunks.Add(lastChunk);
        spawnedPositions.Add(noTerrainPosition);
    }

    private void ChunkOptimization()
    {
        optimizerCooldown -= Time.deltaTime;

        if(optimizerCooldown < 0)
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            if (chunk == null) continue;

            float distance = Vector3.Distance(player.transform.position, chunk.transform.position);

            if (distance > maxChunkDistance)
                chunk.SetActive(false);
            else
                chunk.SetActive(true);
        }
    }
}
