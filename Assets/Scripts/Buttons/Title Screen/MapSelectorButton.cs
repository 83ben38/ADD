using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectorButton : Selectable
{
    private TextMeshPro tmp;
    public MapScriptableObject map;
    private Material _material;
    public int mapNum;

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
        SelectionData.data.map = map;
        SelectionData.data.selectedMap = mapNum;
        MapSelectionManager.manager.end();
        DifficultySelectionManager.manager.startDifficultySelection();
    }

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        tmp = GetComponentInChildren<TextMeshPro>();
        tmp.text = map.mapName;
        _material.color = ColorManager.manager.tile;
    }

    
}
