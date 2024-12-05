using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BubbleShield", menuName = "Skills/2nd Tree/BubbleShield")]
public class BubbleShieldSkill : SkillSO
{
    public override void HandleEffect()
    {
        GameObject.FindWithTag("Player").transform.Find("Bubble").gameObject.SetActive(true);
    }
}
