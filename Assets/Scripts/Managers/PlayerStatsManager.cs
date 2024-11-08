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
    internal float pierce;
    internal float range;

    [Header("For Summoners")]
    internal int maxNrOfMinions;
    internal float timeBetweenSummons;

    [Header("For Porjectiles")]
    internal float projectileSpeed;

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
        maxNrOfMinions = stats.maxNrOfMinions;
        timeBetweenSummons = stats.timeBetweenSummons;
        projectileSpeed = stats.projectileSpeed;

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
                pierce += amount;
                break;
            case StatType.Range:
                range += amount;
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
    FireRate,
    Pierce,
    Range,
    MaxNrOfMinions,
    TimeBetweenSummons,
    ProjectileSpeed
}