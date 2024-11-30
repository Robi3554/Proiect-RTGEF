using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    public CanvasGroup fadeToBlack;

    public void TransitionIn()
    {
        var fade = fadeToBlack.DOFade(1f, 1f).OnComplete(FadeCompleted);
    }

    public void TransitionOut()
    {
        var fade = fadeToBlack.DOFade(0f, 0f).OnComplete(FadeCompleted);
    }

    public void FadeCompleted()
    {
        Time.timeScale = 0f;
    }
}
