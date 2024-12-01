using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class SkillTreeSlide : MonoBehaviour
{
    public Image skillPanel;

    public void SkillTreeEnter()
    {
        skillPanel.rectTransform.DOAnchorPos(new Vector2(0f, -14f), 1f)
                .SetEase(Ease.OutExpo)
                .SetUpdate(true);
    }

    public void SkillTreeLeave()
    {
        int opt = UnityEngine.Random.Range(0, 4);

        switch (opt)
        {
            case 0:
                skillPanel.rectTransform.anchoredPosition = new Vector2(-2000f, -14f);
                break;
            case 1:
                skillPanel.rectTransform.anchoredPosition = new Vector2(2000f, -14f);
                break;
            case 2:
                skillPanel.rectTransform.anchoredPosition = new Vector2(0f, -1200f);
                break;
             case 3:
                skillPanel.rectTransform.anchoredPosition = new Vector2(0f, 1200f);
                break;
        }
    }
}
