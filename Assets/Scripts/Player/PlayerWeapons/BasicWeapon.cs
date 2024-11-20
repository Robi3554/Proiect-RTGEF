using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    private int ShootCount
    {
        get { return shootCount; }
        set
        {
            if (shootCount != value)
            {
                shootCount = value;
                //CheckActivation();
            }
        }
    }

    protected Transform firePoint;

    protected float nextFireTime;
    protected float damage;
    protected float fireRate;
    protected float range;
    protected float criticalMult;
    protected int criticalRate;
    protected int enemyHit;
    protected int shootCount;

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
        enemyHit = PlayerStatsManager.Instance.enemyHit;
        range = PlayerStatsManager.Instance.range;
        criticalMult = PlayerStatsManager.Instance.criticalMult;
        criticalRate = PlayerStatsManager.Instance.criticalRate;
        ShootCount = PlayerStatsManager.Instance.shootCount;
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
