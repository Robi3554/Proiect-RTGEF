using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject button;

    public int level;
    public float expCount;
    public float maxExpNedeed;

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

    void Update()
    {
        if(expCount > maxExpNedeed)
        {
            float aux = expCount - maxExpNedeed;

            LevelUp();

            maxExpNedeed += Mathf.Round(maxExpNedeed * 0.20f);

            expCount = 0 + aux;
        }
    }

    private void LevelUp()
    {
        Debug.Log("You Leveled up!");

        button.SetActive(true);

        Time.timeScale = 0f;

        level++;
    }
}
