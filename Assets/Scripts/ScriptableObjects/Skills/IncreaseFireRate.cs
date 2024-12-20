using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseFireRate", menuName = "Skills/1st Tree/IncreaFireRate")]
public class IncreaseFireRate : SkillSO
{
    public float fireRateIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.FireRate, fireRateIncrease);
    }
}
