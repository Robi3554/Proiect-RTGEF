using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseMaxHealth", menuName = "SkillTree/Skills/IncreaseMaxHealth")]
public class IncreaseMaxHealth : SkillSO
{
    public float healthIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.Health, healthIncrease);
    }
}
