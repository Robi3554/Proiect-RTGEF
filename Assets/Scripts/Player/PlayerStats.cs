using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerSO;

    [Header("Player Stats")]
    internal float health;
    internal float moveSpeed;

    [Header("Weapon Stats")]
    internal float fireRate;
    internal float damage;

    void Awake()
    {
        health = playerSO.health;
        moveSpeed = playerSO.moveSpeed;
        fireRate = playerSO.fireRate;
        damage = playerSO.damage; 
    }
}
