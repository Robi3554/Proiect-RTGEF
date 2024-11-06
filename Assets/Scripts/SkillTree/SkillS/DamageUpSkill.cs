using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageUp", menuName = "SkillTree/Skills/DamageUp")]
public class DamageUpSkill : SkillSO
{
    public float damageIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.Damage, damageIncrease);
    }
}
