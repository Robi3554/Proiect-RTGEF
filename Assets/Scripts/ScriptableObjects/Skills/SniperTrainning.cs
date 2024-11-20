using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SniperTrainning", menuName = "Skills/1st Tree/SniperTrainning")]
public class SniperTrainning : SkillSO
{
    public float damageIncrease;
    public float rangeIncrease;
    public float fireRateDecrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.Damage, damageIncrease);
        PlayerStatsManager.Instance.ChangeStat(StatType.Range, rangeIncrease);
        PlayerStatsManager.Instance.ChangeStat(StatType.FireRate, fireRateDecrease);
    }
}
