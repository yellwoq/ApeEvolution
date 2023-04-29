using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "AudioManager";
                    instance = go.AddComponent<AudioManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public AudioSource backgroundMusic;
    public AudioSource soundEffect;
    private void Start()
    {
        PlayBackgroundMusic();
    }
    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void PlaySoundEffect()
    {
        soundEffect.Play();
    }

}
