using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSO : ScriptableObject
{
    [TextArea]
    public string skillDescription;
    public int maxLevel;
    public int pointsPerLevel;
    public Sprite skillIcon;

    public virtual void HandleEffect() { }
}
