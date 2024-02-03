using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonController : Selectable
{
    private Material _material;
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = ColorManager.manager.path;
    }

    public override void MouseEnter()
    {
        _material.color = ColorManager.manager.pathHighlighted;
    }

    public override void MouseExit()
    {
        _material.color = ColorManager.manager.path;
    }

    public override void MouseClick()
    {
        Application.Quit();
    }
}
