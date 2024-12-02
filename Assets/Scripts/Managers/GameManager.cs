using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SkillTreeSlide slide;

    public Slider expSlider;

    public GameObject[] playerPrefabs;

    private int characterIndex;

    public int score;

    [Header("For leveling")]
    public float expCount = 0;
    public int maxExpNeeded = 100;
    public int level = 1;
    public int availablePoints;
    public bool isPaused;
    private bool isLevelingUp = false;
    private bool continueLevelUp = false;

    public event Action<int> OnLevelUp;

    private void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GameObject player = Instantiate(playerPrefabs[characterIndex], transform.position, Quaternion.identity);

        player.name = playerPrefabs[characterIndex].name;

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

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    private IEnumerator LevelUpRoutine()
    {
        isLevelingUp = true;

        TimeManager.Instance.StopTimer();

        while (expCount >= maxExpNeeded)
        {
            float excessExp = expCount - maxExpNeeded;

            LevelUp();

            maxExpNeeded += Mathf.RoundToInt(maxExpNeeded * 0.20f);

            expCount = excessExp;

            UpdateUI();

            Time.timeScale = 0f;

            continueLevelUp = false;

            slide.SkillTreeEnter();

            yield return new WaitUntil(() => continueLevelUp);

            Time.timeScale = 1f;
        }

        isLevelingUp = false;

        TimeManager.Instance.StartTimer();

        isPaused = false;
    }

    public void AddExp(float ammount)
    {
        expCount += ammount;

        if (!isLevelingUp && expCount >= maxExpNeeded)
        {
            StartCoroutine(LevelUpRoutine());
            isPaused = true;
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        IncreaseScore(level * 100);
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
