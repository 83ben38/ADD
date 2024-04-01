using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumeUpButton : Selectable
{
    [SerializeField]
    public float Volumechange = .1f;
    private Material _material;
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = ColorManager.manager.tile;
    }

    public override void MouseEnter()
    {
        _material.color = ColorManager.manager.tileHighlighted;
    }

    public override void MouseExit()
    {
        _material.color = ColorManager.manager.tile;
    }

    public override void MouseClick()
    {
        SoundEffectsManager.manager.setSoundLevel(Volumechange);
        SoundEffectsManager.manager.playSound("zap");
    }
}
