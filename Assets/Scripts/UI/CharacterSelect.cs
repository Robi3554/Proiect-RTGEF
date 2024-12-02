using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] players;

    public int selectedCharacter;

    private void Awake()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        foreach(GameObject player in players)
        {
            player.SetActive(false);
        }

        players[selectedCharacter].SetActive(true);
    }

    public void ChangeNext()
    {
        players[selectedCharacter].SetActive(false);

        selectedCharacter++;

        if(selectedCharacter == players.Length)
        {
            selectedCharacter = 0;
        }

        players[selectedCharacter].SetActive(true);

        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

    public void ChangePrevious()
    {
        players[selectedCharacter].SetActive(false);

        selectedCharacter--;

        if (selectedCharacter == -1)
        {
            selectedCharacter = players.Length - 1;
        }

        players[selectedCharacter].SetActive(true);

        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }
}
