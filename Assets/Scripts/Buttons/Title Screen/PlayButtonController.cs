using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonController : Selectable
{
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
}
