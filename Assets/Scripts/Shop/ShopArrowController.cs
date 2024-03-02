using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopArrowController : Selectable
{
    public bool right;
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
        ShopController s = ShopController.manager;
        if (right)
        {
            if (s.screenNum < s.availableCrates.Length-1)
            {
                s.screenNum++;
                s.setEnabled();
            }
        }
        else
        {
            if (s.screenNum > 0)
            {
                s.screenNum--;
                s.setEnabled();
            }
        }
        
    }
}

