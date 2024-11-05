using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject skillPanel;

    public Slider expSlider;

    public float expCount = 0;
    public int maxExpNeeded = 100;
    public int level = 1;
    public int availablePoints;

    private bool isLevelingUp = false;
    private bool continueLevelUp = false;

    public event Action<int> OnLevelUp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateUI();
    }

    private IEnumerator LevelUpRoutine()
    {
        isLevelingUp = true;

        while (expCount >= maxExpNeeded)
        {
            float excessExp = expCount - maxExpNeeded;

            LevelUp();

            maxExpNeeded += Mathf.RoundToInt(maxExpNeeded * 0.20f);

            expCount = excessExp;

            UpdateUI();

            Time.timeScale = 0f;

            continueLevelUp = false;
            skillPanel.SetActive(true);

            yield return new WaitUntil(() => continueLevelUp);

            Time.timeScale = 1f;
        }

        isLevelingUp = false;
    }

    public void AddExp(float ammount)
    {
        expCount += ammount;

        if (!isLevelingUp && expCount >= maxExpNeeded)
        {
            StartCoroutine(LevelUpRoutine());
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        Debug.Log("You leveled up!");
        OnLevelUp?.Invoke(InvestmentPointsToAdd() + 5);
        level++;
    }

    private void UpdateUI()
    {
        expSlider.maxValue = maxExpNeeded;
        expSlider.value = expCount;
    }

    public void ContinueLevelUp()
    {
        continueLevelUp = true;
    }

    private int InvestmentPointsToAdd() =>  availablePoints / 5;
}
