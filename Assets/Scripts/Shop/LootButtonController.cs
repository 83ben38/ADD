using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LootButtonController : Selectable
{
    public TextMeshPro text;
    public LootCrateScriptableObject crate;
    private Material _material;
    public GameObject crateObject;
    public void SetUp()
    {
        text.text = crate.cost + " \u20b5\u00a2";
        if (crate.cost >= 10000)
        {
            text.text = (crate.cost/1000) + ((crate.cost%1000)/100) + " \u20b5\u00a2";
        }
    }
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
        
    }
}
