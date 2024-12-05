using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldUpgrade", menuName = "Skills/2nd Tree/ShieldUpgrade")]
public class ShieldUpgrade : SkillSO
{
    public float timeDecrease;

    public float healthIncrease;

    public override void HandleEffect()
    {
        PlayerStatsManager.Instance.ChangeStat(StatType.Health, healthIncrease);

        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BubbleShield>().timeBetweenActivations -= timeDecrease;
    }
}
