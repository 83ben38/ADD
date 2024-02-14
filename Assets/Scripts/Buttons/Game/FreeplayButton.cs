using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeplayButton : Selectable
{
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
        WinController.controller.gameObject.SetActive(false);
    }
}
