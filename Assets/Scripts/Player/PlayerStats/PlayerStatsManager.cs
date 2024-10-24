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
    internal float moveSpeed;

    [Header("Weapon Stats")]
    internal float damage;
    internal float fireRate;
    internal float range;

    [Header("For Summoners")]
    internal int maxNrOfMinions;
    internal float timeBetweenSummons;

    [Header("For Porjectiles")]
    internal float projectileSpeed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        string playerName = GameObject.FindGameObjectWithTag("Player").name;

        foreach (PlayerScriptableObject stats in playerStatsList)
        {

            if (string.Equals(stats.playerName, playerName, StringComparison.OrdinalIgnoreCase))
            {
                health = stats.health;
                moveSpeed = stats.moveSpeed;
                damage = stats.damage;
                fireRate = stats.fireRate;
                range = stats.range;
                maxNrOfMinions = stats.maxNrOfMinions;
                timeBetweenSummons = stats.timeBetweenSummons;
                projectileSpeed = stats.projectileSpeed;
            }
        }
    }
}
