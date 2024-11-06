using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoBox : MonoBehaviour
{
    private SkillSlot skillSlot;

    [SerializeField]
    private TMP_Text infoText;
    [SerializeField]
    private TMP_Text costText;

    private void Awake()
    { 
        skillSlot = GetComponentInParent<SkillSlot>();
    }

    private void Start()
    {
        infoText.text = skillSlot.skillSO.skillDescription;
        costText.text = "Cost : " + skillSlot.skillSO.pointsPerLevel.ToString();
    }
}
