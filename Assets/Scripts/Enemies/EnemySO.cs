using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Data/Enemy Data")]

public class EnemySO : ScriptableObject
{
    public float health;
    public float damage;
    public float speed;
}
