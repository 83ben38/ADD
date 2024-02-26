using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeplayButton : Selectable
{
    public static FreeplayButton button;
    private Material _material;
    public bool enabled = false;
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = ColorManager.manager.tile;
        button = this;
    }

    private void OnEnable()
    {
        if (enabled)
        {
            gameObject.SetActive(false);
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
        enabled = true;
        WinController.controller.gameObject.SetActive(false);
    }
}
