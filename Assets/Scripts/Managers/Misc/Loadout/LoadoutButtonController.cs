using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadoutButtonController : Selectable
{
    public LoadoutButtonController[] other;
    private Material _material;
    public Color? c1, c2 = null;
    public TextMeshPro text;
    public int loadoutNum;
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        text = GetComponentInChildren<TextMeshPro>();
        c1 ??= ColorManager.manager.path;
        c2 ??= ColorManager.manager.pathHighlighted;
        _material.color = (Color)c1;
    }
    public override void MouseEnter()
    {
        for (int i = 0; i < other.Length; i++)
        {
            other[i]._material.color = (Color)c2;
        }
        
    }

    public override void MouseExit()
    {
        for (int i = 0; i < other.Length; i++)
        {
            other[i]._material.color = (Color)c1;
        }
    }

    public override void MouseClick()
    {
        LoadoutButtonController[] buttons = LoadoutSelectionManager.currentButtons;
        for (int i = 0; i < other.Length; i++)
        {
            other[i].c1 = ColorManager.manager.tile;
            other[i].c2 = ColorManager.manager.tileHighlighted;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].c1 = ColorManager.manager.path;
            buttons[i].c2 = ColorManager.manager.pathHighlighted;
        }
        buttons[0].MouseExit();
        LoadoutSelectionManager.currentButtons = other;
        SaveData.save.setLoadoutSelected(loadoutNum);
        MouseEnter();
    }
}
