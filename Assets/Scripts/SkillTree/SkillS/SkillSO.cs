using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSO : ScriptableObject
{
    public string skillName;
    [TextArea]
    public string skillDescription;
    public int maxLevel;
    public int pointsPerLevel;
    public Sprite skillIcon;

    public virtual void HandleEffect() { }
}
