using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public FadeToBlack fade;

    public TMP_Text timeText;
    public TMP_Text scoreText;

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

    public void StartDeathScreen()
    {
        fade.TransitionIn();
        TimeManager.Instance.StopTimer();
        timeText.text = "Time : \n" + TimeManager.Instance.timerText.text;
        scoreText.text = "Score : \n" + GameManager.Instance.score.ToString();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
