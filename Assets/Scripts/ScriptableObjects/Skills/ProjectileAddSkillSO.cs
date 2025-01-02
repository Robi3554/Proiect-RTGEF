using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddProjectile", menuName = "Skills/4th Tree/AddProjectile")]
public class ProjectileAddSkillSO : SkillSO
{
    public GameObject projectile;

    public int chanceToAdd = 5;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.SpecialProjectileChance, chanceToAdd);

        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ProjectileWeapon>().AddSpecialProjectile(projectile);
    }
}
