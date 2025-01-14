using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSummon : MonoBehaviour
{
    public Transform[] spots;

    public GameObject objToSummon;

    private float timeBetweenSummons = 2;
    private int maxNrOfMinions = 3;

    void Start()
    {
        GetStats();

        StartCoroutine(Summoning());

        Minion.OnMinionDestroyed += OnMinionDestroyed;

        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged += GetStats;
        }
        else
        {
            Debug.LogError("PlayerStatsManager.Instance is null in Start!");
        }
    }

    private IEnumerator Summoning()
    {
        for (int i = 0; i < maxNrOfMinions; i++)
        {
            InstantiateMinion(i);
            yield return new WaitForSeconds(timeBetweenSummons);
        }
    }

    private void InstantiateMinion(int index)
    {
        if (index < spots.Length)
        {
            GameObject minion = Instantiate(objToSummon, transform.position, Quaternion.identity);

            Minion movementScript = minion.GetComponent<Minion>();
            if (movementScript != null)
            {
                movementScript.targetSpot = spots[index];
                movementScript.mainShip = transform;
            }
        }
    }

    private void GetStats()
    {
        timeBetweenSummons = PlayerStatsManager.Instance.timeBetweenSummons;
        maxNrOfMinions = PlayerStatsManager.Instance.maxNrOfMinions;
    }

    private void OnMinionDestroyed()
    {
        StartCoroutine(Summoning());
    }

    private void OnDestroy()
    {
        Minion.OnMinionDestroyed -= OnMinionDestroyed;

        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged -= GetStats;
        }

        StopCoroutine(Summoning());
    }
}
