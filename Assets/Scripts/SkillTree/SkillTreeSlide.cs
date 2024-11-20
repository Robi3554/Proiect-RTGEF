using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class SkillTreeSlide : MonoBehaviour
{
    public Image skillPanel;

    public GameObject[] skillTrees;

    private void Start()
    {
        string playerName = GameObject.FindGameObjectWithTag("Player").name;

        foreach (GameObject skillTree in skillTrees) 
        {
            if(string.Equals(playerName, RemoveLastWord(skillTree.name), StringComparison.OrdinalIgnoreCase))
            {
                skillTree.SetActive(true);
            }
        }
    }

    private string RemoveLastWord(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        string[] words = input.Split(' ');

        if (words.Length <= 1) return string.Empty;

        return string.Join(" ", words, 0, words.Length - 1);
    }

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
