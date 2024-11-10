using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncreasePierceAndRange", menuName = "SkillTree/Skills/IncreasePierceAndRange")]
public class IncreasePierceAndRange : SkillSO
{
    public float rangeIncrease;

    public int enemyHitIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.EnemyHit, enemyHitIncrease);

        PlayerStatsManager.Instance.ChangeStat(StatType.Range, rangeIncrease);
    }
}
