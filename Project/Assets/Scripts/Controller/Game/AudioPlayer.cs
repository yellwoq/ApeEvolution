using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApeEvolution
{
  //*************
  //Author:huang
  //Time:2023-2-14
  //…˘“Ù≤•∑≈
  //**********
  public class AudioPlayer : MonoBehaviour
 {
        public AudioSource bgmSource;
        public AudioSource sfxSource;
        private void PlayBG(AudioClip bgm)
        {
            bgmSource.clip = bgm;
            bgmSource.Play();
        }

        private void PlaySfx(AudioClip sfx)
        {
            sfxSource.PlayOneShot(sfx);
        }
    }
}
