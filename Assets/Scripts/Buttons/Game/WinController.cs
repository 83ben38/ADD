using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinController : Selectable
{
    public static WinController controller;
    private Material _material;
    public TextMeshPro text;
    void Start()
    {
        _material = GetComponent<Renderer>().material;
        controller = this;
        gameObject.SetActive(false);
    }

    public void go(bool win)
    {
        if (win)
        {
            text.SetText("You Win!");
        }
        else
        {
            text.SetText("You Lose!");
        }
        gameObject.SetActive(true);
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
