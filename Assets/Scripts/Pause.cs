using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject button;

    public void Unpause()
    {
        Time.timeScale = 1;

        button.SetActive(false);
    }
}
