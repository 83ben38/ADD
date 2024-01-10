using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ColorManager : MonoBehaviour
{
    public static ColorManager manager;

    public Color tile;

    public Color tileHighlighted;

    public Color wall;

    public Color wallHighlighted;

    public Color path;

    public Color pathHighlighted;
    public Color tower;
    public Color towerHighlighted;
    public Color earthTower;
    public Color iceTower;
    public Color fireTower;
    public Color lightningTower;
    public Color waterTower;
    public GameObject projectile;
    
    void Awake()
    {
        manager = this;
        TowerCode.projectile = projectile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
