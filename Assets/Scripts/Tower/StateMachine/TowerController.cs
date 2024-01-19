using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerController : Selectable
{
    
    [Header("Color")]
    private Material _material;
    public Color _baseColor;
    public Color _highlightColor;
    public Color[] baseColors = new Color[4];
    [Header("State")]
    [SerializeReference]
    public TowerState state;
    [Header("Data")]
    public bool block = false;
    public bool wall = false;
    public bool selected = false;
    public bool editable = true;
    public int x, y;
    [Header("Pathfinder Data")] 
    public int minDist;
    public List<TowerController> nextTo = new List<TowerController>();
    [Header("Tower")] 
    public TowerVisual towerVisual;

    public TowerRangeController TRC;
    [SerializeReference]
    public TowerCode tower;

    public int tileType;

    public void setTileType(int num)
    {
        tileType = num;
        
    }

    private void Start()
    {
        
        state = new BeforeGameState();
        _material = GetComponentInChildren<Renderer>().material;
        setBaseColor(ColorManager.manager.tile,ColorManager.manager.tileHighlighted);
        
        TRC = GetComponentInChildren<TowerRangeController>();
        TRC.tower = tower;
        doTileTypeStuff();
    }

    public void setBaseColor(bool path)
    {
        if (path)
        {
            setBaseColor(baseColors[2],baseColors[3]);
        }
        else
        {
            setBaseColor(baseColors[0],baseColors[1]);
        }
        
    }

    public void doTileTypeStuff()
    {
        if (tileType == 0)
        {
            baseColors[0] = ColorManager.manager.tile;
            baseColors[1] = ColorManager.manager.tileHighlighted;
            baseColors[2] = ColorManager.manager.path;
            baseColors[3] = ColorManager.manager.pathHighlighted;
        }

        if (tileType > 0)
        {
            editable = false;
        }

        if (tileType == 1)
        {
            wall = true;
            block = true;
            setBaseColor(ColorManager.manager.wallPerm, ColorManager.manager.wallPermHighlighted);
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(scale.x, scale.y*2, scale.z);
        }

        if (tileType == 2)
        {
            baseColors[0] = ColorManager.manager.tilePerm;
            baseColors[1] = ColorManager.manager.tilePermHighlighted;
            baseColors[2] = ColorManager.manager.pathPerm;
            baseColors[3] = ColorManager.manager.pathPermHighlighted;
            setBaseColor(false);
        }

        if (tileType > 2)
        {
            Color c = ColorManager.manager.portal1;
            Color c1 = ColorManager.manager.portal1Highlighted;
            baseColors[0] = c;
            baseColors[1] = c1;
            baseColors[2] = c;
            baseColors[3] = c1;
            setBaseColor(false);
        }
    }
    

    private void FixedUpdate()
    {
        if (state != null)
        {
            state.Run(this);
        }
        TRC.tower = tower;
    }
    

    #region color

    public void setBaseColor(Color c, Color c2)
    {
        _baseColor = c;
        _highlightColor = c2;
        if (selected)
        {
            setColor(_highlightColor);
        }
        else
        {
            setColor(_baseColor);
        }
    }

    public void setColor(Color c)
    {
        _material.color = c;
    }

    #endregion

    #region input

    public override void MouseEnter()
    {
        setColor(_highlightColor);
        selected = true;
        TRC.isVisibal = true;
        
    }

    public override void MouseExit()
    {
        setColor(_baseColor);
        selected = false;
        TRC.isVisibal = false;
    }

    public override void MouseClick()
    {
        if (state != null)
        {
            state.MouseClick(this);
        }
    }

    #endregion
    
}
