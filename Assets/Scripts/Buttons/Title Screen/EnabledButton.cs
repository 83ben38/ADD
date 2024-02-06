using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledButton : Selectable
{
    public Material _material;
    public Color c1, c2;


    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    public override void MouseEnter()
    {
        _material.color = c2;
    }

    public override void MouseExit()
    {
        _material.color = c1;
    }

    public override void MouseClick()
    {
        TowerDescriptionManager.manager.setEnabled();
    }
}
