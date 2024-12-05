using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthRegen", menuName = "Skills/2nd Tree/HealthRegen")]
public class HealthRegen : SkillSO
{
    public float regenIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.RegenPerSec, regenIncrease);
    }
}
