using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        int d = 0;
        int[] loadout = LoadoutManager.manager.loadouts[SaveData.save.getLoadoutSelected()].loadout;
        for (int i = 0; i < loadout.Length; i++)
        {
            d += loadout[i];
        }
        SelectionData.data.towerCodes = new int[d];
        int z = 0;
        for (int i = 0; i < TowerSelectionManager.selected.Length; i++)
        {
            TowerController c = TowerSelectionManager.selected[i];
            if (c.tower == null)
            {
                return;
            }

            for (int j = 0; j < loadout[i]; j++)
            {
                SelectionData.data.towerCodes[z] = c.x;
                z++;
            }
        }

        SceneManager.LoadScene("Game");
    }
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = ColorManager.manager.tile;
    }
}
