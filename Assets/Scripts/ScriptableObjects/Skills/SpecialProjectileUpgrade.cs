using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialProjectileUpgrade", menuName = "Skills/4th Tree/SpecialProjectileUpgrade")]
public class SpecialProjectileUpgrade : SkillSO
{
    public int amountToChainAdd;
    public int explsoionModifierAdd;

    public override void HandleEffect() 
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.AmountToChainLightning, amountToChainAdd);
        PlayerStatsManager.Instance.ChangeStat(StatType.ProjectileExplosionModifier, explsoionModifierAdd);

        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ProjectileWeapon>().onlySpeciaProjectiles = true;
    }
}
