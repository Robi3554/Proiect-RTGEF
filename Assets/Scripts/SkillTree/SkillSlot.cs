using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour
{
    [SerializeField]
    private InfoBox infoBox;
    [SerializeField]
    private List<SkillSlot> prerequisiteSkillSlots;
    public SkillSO skillSO;

    public Image skillIcon;
    public TMP_Text skillLevelText;
    public RectTransform levelBack;
    public Button skillButton;

    private int currentLevel;
    public bool isUnlocked;

    public float lockedSize;
    public float unlockedSize;

    public static event Action<SkillSlot> OnAbilityPointsSpent;
    public static event Action<SkillSlot> OnSkillMaxed;

    private void OnValidate()
    {
        if (ValidationChecks())
        {
            EditorApplication.delayCall += () =>
            {
                if (this != null)
                {
                    UpdateUI();
                }
            };
        }
    }

    private void Start()
    {
        skillIcon.sprite = skillSO.skillIcon;
    }

    private void UpdateUI()
    {
        if (isUnlocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
            levelBack.sizeDelta = new Vector2(unlockedSize, levelBack.sizeDelta.y);
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.gray;
            levelBack.sizeDelta = new Vector2(lockedSize, levelBack.sizeDelta.y);
        }
    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            currentLevel++;
            OnAbilityPointsSpent?.Invoke(this);
            skillSO.HandleEffect();
            
            if(currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }

            UpdateUI();
        }
    }

    public bool CanBeUnlocked()
    {
        foreach(SkillSlot slot in prerequisiteSkillSlots)
        {
            if(!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
            {
                return false;
            }
        }

        return true;
    }

    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }

    public void OnHover()
    {
        infoBox.gameObject.SetActive(true);
    }

    public void OnHoverOver()
    {
        infoBox.gameObject.SetActive(false);
    }

    private bool ValidationChecks() => skillSO != null && skillLevelText != null && levelBack != null;
}
