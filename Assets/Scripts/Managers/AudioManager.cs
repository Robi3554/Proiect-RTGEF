using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
     private AudioSource 
          musicSource,
          sfxSource;

    [Header("Audio Clips")]
    public AudioClip music;
    public AudioClip shooting;

    private void Start()
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

        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySFXClip(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
