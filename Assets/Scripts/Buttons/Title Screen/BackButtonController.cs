using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonController : Selectable
{
    private Material _material;
    public MapSelectionManager mapDisable;
    public DifficultySelectionManager difficultyDisable;
    public List<GameObject> enable;
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
        MapSelectionManager.manager.startMapSelection();
        if (difficultyDisable == null)
        {
            mapDisable.end();
        }
        else
        {
            difficultyDisable.end();
        }

        if (enable.Count == 0)
        {
            mapDisable.startMapSelection();
        }
        else
        {
            foreach (var gameObject in enable)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
