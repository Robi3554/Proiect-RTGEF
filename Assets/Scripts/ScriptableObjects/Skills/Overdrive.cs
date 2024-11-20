using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Overdrive", menuName = "Skills/1st Tree/Overdrive")]
public class Overdrive : SkillSO
{
    public float damageIncrease;
    public float fireRateIncrease;
    public float moveSpeedIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.Damage, damageIncrease);
        PlayerStatsManager.Instance.ChangeStat(StatType.FireRate, fireRateIncrease);
        PlayerStatsManager.Instance.ChangeStat(StatType.MoveSpeed, moveSpeedIncrease);
    }
}
