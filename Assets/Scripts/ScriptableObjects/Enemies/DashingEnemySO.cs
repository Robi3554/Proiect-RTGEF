using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Data/Enemy Data/Dashing Enemy")]
public class DashingEnemySO : EnemySO
{
    public float dashingPower;
    public float dashingCooldown;
}
