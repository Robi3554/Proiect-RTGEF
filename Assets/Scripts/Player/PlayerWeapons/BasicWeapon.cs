using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public Transform firePoint;

    protected float nextFireTime;
    protected float damage;
    protected float fireRate;
    protected float criticalMult;
    protected int criticalRate;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        firePoint = gameObject.transform;

        GetStats();


        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged += GetStats;
        }
        else
        {
            Debug.LogError("PlayerStatsManager.Instance is null in Start!");
        }
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Shoot()
    {
        
    }

    protected virtual void GetStats()
    {
        damage = PlayerStatsManager.Instance.damage;
        fireRate = PlayerStatsManager.Instance.fireRate;
        criticalMult = PlayerStatsManager.Instance.criticalMult;
        criticalRate = PlayerStatsManager.Instance.criticalRate;
    }

    protected float CheckDamage(float damage)
    {
        if(criticalRate > Random.Range(0, 100))
        {
            return damage * criticalMult;
        }
        else
        {
            return damage;
        }
    }

    protected virtual void OnDestroy()
    {
        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.OnStatsChanged -= GetStats;
        }
    }
}
