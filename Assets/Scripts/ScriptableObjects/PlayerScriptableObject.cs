using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerScriptableObject : ScriptableObject
{
    public string playerName;

    [Header("Player Stats")]
    public float health;
    public float regenPerSec;
    public float moveSpeed;

    [Header("Weapon Stats")]
    public float damage;
    public float fireRate;
    public float range;
    public float ciritcalMult;
    public int criticalRate;
    public int pierce;
    public int shootCount;

    [Header("For Summoners")]
    public int maxNrOfMinions;
    public float timeBetweenSummons;

    [Header("For Projectiles")]
    public float projectileSpeed;
    public int projectileCount;
    public int specialProjectileChance;
    public int projectileExplosionModifier;
    public int amountToChainLightning;
}
