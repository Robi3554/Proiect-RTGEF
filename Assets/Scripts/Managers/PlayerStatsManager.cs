using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public PlayerScriptableObject[] playerStatsList;

    public static PlayerStatsManager Instance;

    [Header("Player Stats")]
    internal float health;
    internal float regenPerSec;
    internal float moveSpeed;

    [Header("Weapon Stats")]
    internal float damage;
    internal float fireRate;
    internal float range;
    internal float criticalMult;
    internal int criticalRate;
    internal int pierce;
    internal int shootCount;

    [Header("For Summoners")]
    internal float timeBetweenSummons;
    internal int maxNrOfMinions;

    [Header("For Porjectiles")]
    internal float projectileSpeed;
    internal int projectileCount;
    internal int specialProjectileChance;
    internal int projectileExplosionModifier;
    internal int amountToChainLightning;

    public event Action OnStatsChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        string playerName = GameObject.FindGameObjectWithTag("Player").name;

        foreach (PlayerScriptableObject stats in playerStatsList)
        {

            if (string.Equals(stats.playerName, playerName, StringComparison.OrdinalIgnoreCase))
            {
                InitalStats(stats);
            }
        }
    }

    public void InitalStats(PlayerScriptableObject stats)
    {
        health = stats.health;
        regenPerSec = stats.regenPerSec;
        moveSpeed = stats.moveSpeed;
        damage = stats.damage;
        fireRate = stats.fireRate;
        pierce = stats.pierce;
        range = stats.range;
        criticalMult = stats.ciritcalMult;
        criticalRate = stats.criticalRate;
        shootCount = stats.shootCount;
        maxNrOfMinions = stats.maxNrOfMinions;
        timeBetweenSummons = stats.timeBetweenSummons;
        projectileSpeed = stats.projectileSpeed;
        projectileCount = stats.projectileCount;
        specialProjectileChance = stats.specialProjectileChance;
        projectileExplosionModifier = stats.projectileExplosionModifier;
        amountToChainLightning = stats.amountToChainLightning;

        OnStatsChanged?.Invoke();
    }

    public void ChangeStat(StatType statType, float amount)
    {
        switch (statType)
        {
            case StatType.Health:
                health += amount;
                break;
            case StatType.RegenPerSec:
                regenPerSec += amount;
                break;
            case StatType.MoveSpeed:
                moveSpeed += amount;
                break;
            case StatType.Damage:
                damage += amount;
                break;
            case StatType.FireRate:
                fireRate += amount;
                break;
            case StatType.Pierce:
                pierce += (int)amount;
                break;
            case StatType.ShootCount:
                shootCount += (int)amount;
                break;
            case StatType.Range:
                range += amount;
                break;
            case StatType.CriticalMult:
                criticalMult += amount;
                break;
            case StatType.CriticalRate:
                criticalRate += (int)amount;
                break;
            case StatType.MaxNrOfMinions:
                maxNrOfMinions += (int)amount;
                break;
            case StatType.TimeBetweenSummons:
                timeBetweenSummons += amount;
                break;
            case StatType.ProjectileSpeed:
                projectileSpeed += amount;
                break;
            case StatType.ProjectileCount:
                projectileCount += (int)amount;
                break;
            case StatType.SpecialProjectileChance:
                specialProjectileChance += (int)amount;
                break;
            case StatType.ProjectileExplosionModifier:
                projectileExplosionModifier += (int)amount;
                break;
            case StatType.AmountToChainLightning:
                amountToChainLightning += (int)amount;
                break;
        }

        OnStatsChanged?.Invoke();
    }
}

public enum StatType
{
    Health,
    RegenPerSec,
    MoveSpeed,
    Damage,
    CriticalRate,
    CriticalMult,
    ShootCount,
    FireRate,
    Pierce,
    Range,
    MaxNrOfMinions,
    TimeBetweenSummons,
    ProjectileSpeed,
    ProjectileCount,
    SpecialProjectileChance,
    ProjectileExplosionModifier,
    AmountToChainLightning
}