using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SkillTreeSlide slide;

    public Slider expSlider;

    public GameObject[] playerPrefabs;

    private int characterIndex;

    public int score;

    [Header("Damage Text")]
    public Canvas damageCanvas;
    public float textFontSize;
    public TMP_FontAsset font;
    public Camera referenceCamera;

    [Header("For leveling")]
    public float expCount = 0;
    public int maxExpNeeded = 100;
    public int level = 1;
    public int availablePoints;
    public bool isPaused;
    private bool isLevelingUp = false;
    private bool continueLevelUp = false;

    public event Action<int> OnLevelUp;

    public float enemyStatMultiplier = 1f;

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

    public static void GenerateFloatingText(string text, Transform target, Vector3 hitDirection, float duration = 1f, float speed = 1f)
    {
        if(!Instance.damageCanvas)
        {
            return;
        }

        if (!Instance.referenceCamera)
        {
            Instance.referenceCamera = Camera.main;
        }

        Instance.StartCoroutine(Instance.GenerateFloatingTextCoroutine(text, target, hitDirection, duration, speed));
    }

    private IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, Vector3 hitDirection, float duration = 1f, float speed = 1f)
    {
        GameObject textObj = new GameObject("Damage Floating Text");
        RectTransform rect = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI tmPro = textObj.AddComponent<TextMeshProUGUI>();

        tmPro.text = text;
        tmPro.horizontalAlignment = HorizontalAlignmentOptions.Center;
        tmPro.verticalAlignment = VerticalAlignmentOptions.Middle;
        tmPro.fontSize = textFontSize;
        if (font)
        {
            tmPro.font = font;
        }

        textObj.transform.SetParent(Instance.damageCanvas.transform);
        textObj.transform.SetAsFirstSibling();
        rect.position = referenceCamera.WorldToScreenPoint(target.position);

        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0;

        Vector3 diagonalOffset = hitDirection.normalized * 50f;
        float upwardOffset = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            tmPro.color = new Color(tmPro.color.r, tmPro.color.g, tmPro.color.b, 1 - t / duration);

            upwardOffset += speed * Time.deltaTime;
            if (target != null)
            {
                Vector3 screenPosition = referenceCamera.WorldToScreenPoint(target.position + new Vector3(0, upwardOffset));
                screenPosition += diagonalOffset * (1 - t / duration);
                rect.position = screenPosition;
            }
            else
            {
                rect.position += new Vector3(diagonalOffset.x * Time.deltaTime, speed * Time.deltaTime, 0);
            }
            yield return w;
        }

        Destroy(textObj);
    }

    public void IncreaseMultiplier(float amount)
    {
        enemyStatMultiplier += amount;
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

    private IEnumerator LevelUpRoutine()
    {
        isLevelingUp = true;

        //TimeManager.Instance.StopTimer();

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

        //TimeManager.Instance.StartTimer();

        isPaused = false;
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
