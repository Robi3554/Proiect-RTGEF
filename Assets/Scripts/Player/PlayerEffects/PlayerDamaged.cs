using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem playerDamaged;

    private ParticleSystem playerDamagedInstance;

    public void InstantiateParticles()
    {
        playerDamagedInstance = Instantiate(playerDamaged, transform.position, Quaternion.identity);
    }
}
