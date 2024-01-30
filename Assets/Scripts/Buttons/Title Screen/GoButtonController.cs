using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButtonController : Selectable
{
    private Material _material;
    public List<GameObject> disable;
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
        foreach (var gameObject in disable)
        {
            gameObject.SetActive(false);   
        }
    }
}
