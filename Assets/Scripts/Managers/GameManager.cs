using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject button;

    public int expCount = 0;
    public int maxExpNeeded = 100;
    public int level = 1;

    private bool isLevelingUp = false;
    private bool continueLevelUp = false;


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
        button.SetActive(false);

        button.GetComponent<Button>().onClick.AddListener(ContinueLevelUp);
    }

    void Update()
    {
        if (!isLevelingUp && expCount >= maxExpNeeded)
        {
            StartCoroutine(LevelUpRoutine());
        }
    }

    private IEnumerator LevelUpRoutine()
    {
        isLevelingUp = true;

        while (expCount >= maxExpNeeded)
        {
            int excessExp = expCount - maxExpNeeded;

            LevelUp();

            maxExpNeeded += Mathf.RoundToInt(maxExpNeeded * 0.20f);

            expCount = excessExp;

            button.SetActive(true);
            Time.timeScale = 0f;

            continueLevelUp = false;
            yield return new WaitUntil(() => continueLevelUp);

            button.SetActive(false);
            Time.timeScale = 1f;
        }

        isLevelingUp = false;
    }

    public void AddExp(int ammount)
    {
        expCount += ammount;
    }

    private void LevelUp()
    {
        Debug.Log("You leveled up!");
        level++;
    }

    private void ContinueLevelUp()
    {
        continueLevelUp = true;
    }
}
