using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeSelection : MonoBehaviour
{
    public GameObject[] skillTrees;

    private void Start()
    {
        string playerName = GameObject.FindGameObjectWithTag("Player").name;

        foreach (GameObject skillTree in skillTrees)
        {
            if (string.Equals(playerName, RemoveLastWord(skillTree.name), StringComparison.OrdinalIgnoreCase))
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
}
