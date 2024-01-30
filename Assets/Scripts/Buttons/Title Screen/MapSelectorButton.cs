using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapSelectorButton : Selectable
{
    private TextMeshPro tmp;
    public string text;
    private Material _material;

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

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        tmp = GetComponentInChildren<TextMeshPro>();
        tmp.text = text;
    }

    
}
