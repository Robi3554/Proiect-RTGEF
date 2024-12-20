using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncreasePierceAndRange", menuName = "Skills/1st Tree/IncreasePierceAndRange")]
public class IncreasePierceAndRange : SkillSO
{
    public float rangeIncrease;

    public int pierceIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.Pierce, pierceIncrease);

        PlayerStatsManager.Instance.ChangeStat(StatType.Range, rangeIncrease);
    }
}
