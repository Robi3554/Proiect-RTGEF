using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]   
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    public void TriggerDeath()
    {
        float randomNumber = Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();

        foreach (Drops drop in drops)
        {
            if (randomNumber <= drop.dropRate)
            {
                possibleDrops.Add(drop);
            }
        }
        if (possibleDrops.Count > 0)
        {
            Drops drop = possibleDrops[Random.Range(0, possibleDrops.Count)];
            Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
