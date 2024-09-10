using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButtonController : Selectable
{
    public bool right;
    public bool loadout = false;
    private Material _material;
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = ColorManager.manager.tile;
        if (loadout)
        {
            if (LoadoutSelectionManager.allButtons.Length == 1)
            {
                gameObject.SetActive(false);
            }
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
        if (loadout)
        {
            if (right)
            {
                LoadoutSelectionManager.screen++;
                if (LoadoutSelectionManager.screen >= LoadoutSelectionManager.allButtons.Length)
                {
                    LoadoutSelectionManager.screen--;
                }
            }
            else
            {
                LoadoutSelectionManager.screen--;
                if (LoadoutSelectionManager.screen < 0)
                {
                    LoadoutSelectionManager.screen++;
                }
            }

            LoadoutSelectionManager.manager.ResetMap();
        }
        else
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
}
