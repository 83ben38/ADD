using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager manager;
    public float CurrentVol;
    
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

    
    public void setSoundLevel(float CurrentVol)
    {
        source.volume += CurrentVol;
        this.CurrentVol += CurrentVol;
        if (this.CurrentVol > 1)
        {
            source.volume = 1;
            this.CurrentVol = 1;
        }
        if (this.CurrentVol < 0)
        {
            source.volume = 0;
            this.CurrentVol = 0;
        }
    }

}
