using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisibleVolume : Selectable
{
    private Material _material;
    public TextMeshPro text;
    public String Volume;
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
        
    }

    public void Update()
    {
      Volume =  (SoundEffectsManager.manager.CurrentVol*100) + "%";

        text.text = Volume;
    }

}
