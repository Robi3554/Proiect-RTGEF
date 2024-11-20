using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseShootCount", menuName = "Skills/3rd Tree/RayCast/Increase Shoot Count")]
public class IncreaseShootCount : SkillSO
{
    public int shootCountIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.ShootCount, shootCountIncrease);
    }
}
