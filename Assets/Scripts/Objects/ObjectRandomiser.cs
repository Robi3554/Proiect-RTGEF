using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomiser : MonoBehaviour
{
    public List<GameObject> objectLocations;
    public List<GameObject> objectPrefabs;

    void Start()
    {
        SpawnProps();
    }

    private void SpawnProps()
    {
        foreach(var ol in objectLocations)
        {
            int rand = Random.Range(0, objectPrefabs.Count);
            GameObject obj = Instantiate(objectPrefabs[rand], ol.transform.position, Quaternion.identity);
            obj.transform.parent = ol.transform;
        }
    }
}
