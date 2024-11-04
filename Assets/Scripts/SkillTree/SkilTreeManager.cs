using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SkilTreeManager : MonoBehaviour
{
    public SkillSlot[] skillSlots;

    public TMP_Text pointsText;
    public TMP_Text investmentPointsText;

    private int availablePoints;
    private int investmentPoints;

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

        availablePoints = GameManager.Instance.availablePoints;

        GameManager.Instance.OnLevelUp += UpdateAbilityPoints;

        UpdateAbilityPoints(0);
    }

    private void CheckAvailablePoints(SkillSlot skillSlot)
    {
        if(availablePoints > 0 && availablePoints >= skillSlot.skillSO.pointsPerLevel)
        {
            skillSlot.TryUpgradeSkill();
        }
    }

    private void UpdateAbilityPoints(int amount)
    {
        GameManager.Instance.availablePoints += amount;

        availablePoints = GameManager.Instance.availablePoints;

        pointsText.text = "Total Points : " + availablePoints.ToString();

        investmentPoints = availablePoints / 5;

        investmentPointsText.text = "Investment points  for next round : " + investmentPoints.ToString();
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

        GameManager.Instance.OnLevelUp -= UpdateAbilityPoints;
    }
}
