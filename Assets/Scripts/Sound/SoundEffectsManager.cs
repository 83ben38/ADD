using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    }

}
