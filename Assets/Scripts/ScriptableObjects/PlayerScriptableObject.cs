using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerScriptableObject : ScriptableObject
{
    [Header("Player Stats")]
    public float health;
    public float moveSpeed;

    [Header("Weapin Stats")]
    public float damage;
    public float fireRate;
}
