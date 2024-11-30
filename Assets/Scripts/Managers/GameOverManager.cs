using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public FadeToBlack fade;

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
    }

    public void ReturnToMain()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
