using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowActivePlayer : MonoBehaviour
{
    private CinemachineVirtualCamera vc;
    private GameObject player;

    void Start()
    {
        StartCoroutine(StartFollow());
    }

    private IEnumerator StartFollow()
    {
        yield return null;

        vc = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindWithTag("Player");

        if (vc != null && player != null)
        {
            vc.Follow = player.transform;
        }
    }
}
