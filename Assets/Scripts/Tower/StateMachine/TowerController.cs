using System;
using System.Collections;
using System.Collections.Generic;
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
    [Header("State")]
    [SerializeReference]
    public TowerState state;
    [Header("Data")]
    public bool block = false;
    public bool wall = false;
    public bool selected = false;
    public int x, y;
    [Header("Pathfinder Data")] 
    public int minDist;
    public List<TowerController> nextTo = new List<TowerController>();
    [Header("Tower")] 
    public TowerVisual towerVisual;

    public TowerRangeControler TRC;
    [SerializeReference]
    public TowerCode tower;
    private void Start()
    {
        
        state = new BeforeGameState();
        _material = GetComponentInChildren<Renderer>().material;
        setBaseColor(ColorManager.manager.tile,ColorManager.manager.tileHighlighted);
        
        TRC = GetComponentInChildren<TowerRangeControler>();
        TRC.tower = tower;
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
