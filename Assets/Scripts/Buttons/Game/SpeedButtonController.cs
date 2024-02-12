using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;

public class SpeedButtonController : Selectable
{
    private Material _material;
    public TextMeshPro text;
    public int _speedNum;
    public float[] speeds;
    void Start()
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
        _speedNum++;
        if (_speedNum == speeds.Length)
        {
            _speedNum = 0;
        }

        Time.timeScale = speeds[_speedNum];
        text.text = "Speed: " + Time.timeScale + "x";
    }
    
}
