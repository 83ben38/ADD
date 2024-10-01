using System;
using TMPro;
using UnityEngine;

public class WaveCounter : Selectable
{
    private Material _material;
    public TextMeshPro text;
    public static WaveCounter waveCounter;
    void Start()
    {
        waveCounter = this;
        _material = GetComponent<Renderer>().material;
        _material.color = ColorManager.manager.tile;
    }

    public void UpdateText()
    {
        StartButtonController sb = StartButtonController.startButton;
        if (sb.wave+1 > sb.waves.waves.Length)
        { 
            text.text = "Wave " + (sb.wave);
        }
        else
        {
            text.text = "Wave " + (sb.wave+1) + "/"  + sb.waves.waves.Length;
        }
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
        

    }
    
}