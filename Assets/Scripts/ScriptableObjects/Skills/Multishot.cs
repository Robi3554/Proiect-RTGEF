using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Multishot", menuName = "Skills/3rd Tree/Projectile/Multishot")]
public class Multishot : SkillSO
{
    public int projectileCountIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.ProjectileCount, projectileCountIncrease);
    }
}
