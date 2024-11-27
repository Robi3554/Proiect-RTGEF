using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    public int timeBetweenSummons;

    void Start()
    {
        StartCoroutine(SummonEnemies());
    }

    private IEnumerator SummonEnemies()
    {
        while (true)
        {
            int randNr = Random.Range(0, enemies.Length);

            Instantiate(enemies[randNr], transform.position, Quaternion.identity);

            yield return new WaitForSeconds(timeBetweenSummons);
        }
    }
}
