using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager manager;

    private void Start()
    {
        manager = this;
    }

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip zap;
    [SerializeField]
    private AudioClip splash;
    public void playSound(string sound)
    {
        if (sound.Equals("zap"))
        {
            source.PlayOneShot(zap);
        }
        if (sound.Equals("splash"))
        {
            source.PlayOneShot(splash);
        }
    }

}
