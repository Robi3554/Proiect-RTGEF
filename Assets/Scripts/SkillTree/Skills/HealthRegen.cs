using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthRegen", menuName = "SkillTree/Skills/HealthRegen")]
public class HealthRegen : SkillSO
{
    public float regenIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.RegenPerSec, regenIncrease);
    }
}
