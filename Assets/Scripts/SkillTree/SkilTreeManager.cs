using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SkilTreeManager : MonoBehaviour
{
    public SkillSlot[] skillSlots;

    public TMP_Text pointsText;

    public int availablePoints;

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointsSpent += HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
    }

    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvailablePoints(slot));
        }

        UpdateAbilityPoints(0);
    }

    private void CheckAvailablePoints(SkillSlot slot)
    {
        if(availablePoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }

    private void UpdateAbilityPoints(int amount)
    {
        availablePoints += amount;

        pointsText.text = "Points : " + availablePoints.ToString();
    }

    private void HandleAbilityPointsSpent(SkillSlot skillSlot)
    {
        if(availablePoints > 0 && availablePoints >= skillSlot.skillSO.pointsPerLevel)
        {
            UpdateAbilityPoints(-skillSlot.skillSO.pointsPerLevel);
        }
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach(SkillSlot slot in skillSlots)
        {
            if(!slot.isUnlocked && slot.CanBeUnlocked())
            {
                slot.Unlock();
            }
        }
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointsSpent -= HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
    }
}
