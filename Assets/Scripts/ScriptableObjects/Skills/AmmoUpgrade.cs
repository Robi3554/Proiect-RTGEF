using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoUpgrade", menuName = "Skills/3rd Tree/Projectile/AmmoUpgrade")]
public class AmmoUpgrade : SkillSO
{
    public float damageIncrease;
    public float projectileSpeedIncrease;

    public override void HandleEffect() 
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.Damage, damageIncrease);
        PlayerStatsManager.Instance.ChangeStat(StatType.ProjectileSpeed, projectileSpeedIncrease);
    }
}
