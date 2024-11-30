using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    public CanvasGroup fadeToBlack;

    public void TransitionIn()
    {
        var fade = fadeToBlack.DOFade(1f, 1f);
    }

    public void TransitionOut()
    {
        var fade = fadeToBlack.DOFade(0f, 0f);
    }
}
