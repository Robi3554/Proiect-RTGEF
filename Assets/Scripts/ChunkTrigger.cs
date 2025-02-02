using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private MapController mc;

    public GameObject targetChunk;

    void Start()
    {
        mc = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            mc.currentChunk = targetChunk;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(mc.currentChunk == targetChunk)
            {
                mc.currentChunk = null;
            }
        }
    }
}
