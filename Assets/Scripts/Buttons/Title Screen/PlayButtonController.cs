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
        SelectionData.data.towerCodes = new int[TowerSelectionManager.selected.Length];
        for (int i = 0; i < TowerSelectionManager.selected.Length; i++)
        {
            TowerController c = TowerSelectionManager.selected[i];
            if (c.tower == null)
            {
                return;
            }
            SelectionData.data.towerCodes[i] = c.x;
        }

        SceneManager.LoadScene("Game");
    }
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }
}
