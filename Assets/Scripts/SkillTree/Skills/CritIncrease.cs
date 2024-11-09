using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CritIncrease", menuName = "SkillTree/Skills/CritIncrease")]
public class CritIncrease : SkillSO
{
    public float multIncrease;

    public int rateIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.CriticalMult, multIncrease);
        PlayerStatsManager.Instance.ChangeStat(StatType.CriticalRate, rateIncrease);
    }
}
