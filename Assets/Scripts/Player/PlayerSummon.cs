using Microsoft.Cci;
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
        timeBetweenSummons = PlayerStatsManager.Instance.timeBetweenSummons;
        maxNrOfMinions = PlayerStatsManager.Instance.maxNrOfMinions;

        StartCoroutine(Summoning());

        Minion.OnMinionDestroyed += OnMinionDestroyed;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        DestroyMinion();
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
            }
        }
    }

    private void DestroyMinion()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject minion = GameObject.FindGameObjectWithTag("PlayerMinion");

            Destroy(minion);
        }
    }

    private void OnMinionDestroyed()
    {
        StartCoroutine(Summoning());
    }

    private void OnDestroy()
    {
        Minion.OnMinionDestroyed -= OnMinionDestroyed;

        StopCoroutine(Summoning());
    }
}
