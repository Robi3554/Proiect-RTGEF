using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public void Die()
    {
        GameOverManager.Instance.StartDeathScreen();
        Destroy(gameObject);
    }
}
