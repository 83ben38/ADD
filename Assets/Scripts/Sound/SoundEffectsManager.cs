using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    private AudioClip fire;
    [SerializeField]
    private AudioClip splash;
    [SerializeField]
    private AudioClip ice;
    [SerializeField]
    private AudioClip earth;
    [SerializeField]
    private AudioClip atomic;
     [SerializeField]
    private AudioClip ironDeploy;
    [SerializeField]
    private AudioClip ironHit;
    [SerializeField]
    private AudioClip artilleryFire;
    [SerializeField]
    private AudioClip wind;
    [SerializeField]
    private AudioClip life;
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
        if (sound.Equals("fire"))
        {
            source.PlayOneShot(fire);
        }
        if (sound.Equals("ice"))
        {
            source.PlayOneShot(ice);
        }
        if (sound.Equals("ice"))
        {
            source.PlayOneShot(ice);
        }
        if (sound.Equals("earth"))
        {
            source.PlayOneShot(earth);
        }
        if (sound.Equals("atomic"))
        {
            source.PlayOneShot(atomic);
        }
        if (sound.Equals("iron-deploy"))
        {
            source.PlayOneShot(ironDeploy);
        }
        if (sound.Equals("iron-hit"))
        {
            source.PlayOneShot(ironHit);
        }
        if (sound.Equals("artillery-fire"))
        {
            source.PlayOneShot(artilleryFire);
        }
        if (sound.Equals("wind"))
        {
            source.PlayOneShot(wind, 0.50f);
        }
        if (sound.Equals("life"))
        {
            source.PlayOneShot(life);
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
