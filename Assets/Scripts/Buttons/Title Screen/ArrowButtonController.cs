using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButtonController : Selectable
{
    public bool right;
    private Material _material;
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
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
        if (right)
        {
            MapSelectionManager.manager.screenNum++;
            if (MapSelectionManager.manager.screenNum >= MapSelectionManager.manager.maps.Length)
            {
                MapSelectionManager.manager.screenNum--;
            }
        }
        else
        {
            MapSelectionManager.manager.screenNum--;
            if (MapSelectionManager.manager.screenNum < 0)
            {
                MapSelectionManager.manager.screenNum++;
            }
        }
        MapSelectionManager.manager.setEnabled();
    }
}
