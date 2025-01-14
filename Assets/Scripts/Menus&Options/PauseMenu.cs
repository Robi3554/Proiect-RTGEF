using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isPaused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.isPaused = true;
    }

    public void UnPause()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.isPaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartScene");
    }
}
