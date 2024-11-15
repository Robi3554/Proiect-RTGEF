using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BubbleShield", menuName = "SkillTree/Skills/BubbleShield")]
public class BubbleShieldSkill : SkillSO
{
    public override void HandleEffect()
    {
        GameObject.FindWithTag("Player").transform.Find("Bubble").gameObject.SetActive(true);
    }
}
