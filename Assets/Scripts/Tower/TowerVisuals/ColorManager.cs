using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ColorManager : MonoBehaviour
{
    public static ColorManager manager;
    public Color portal1;
    public Color portal1Highlighted;
    public Color portal2;
    public Color portal2Highlighted;
    public Color portal3;
    public Color portal3Highlighted;
    public Color wallPerm;
    public Color wallPermHighlighted;
    public Color tilePerm;
    public Color tilePermHighlighted;
    public Color pathPerm;
    public Color pathPermHighlighted;
    public Color start;
    public Color startHighlighted;
    public Color end;
    public Color endHighlighted;
    public Color checkpoint;
    public Color checkpointHighlighted;
    public Color tile;

    public Color tileHighlighted;

    public Color wall;

    public Color wallHighlighted;

    public Color path;

    public Color pathHighlighted;
    public Color background;
    public Color tower;
    public Color towerHighlighted;
    public Color earthTower;
    public Color iceTower;
    public Color fireTower;
    public Color lightningTower;
    public Color waterTower;
    public Color ironTower;
    public Color atomicTower;
    public Color spaceTower;
    public Color windTower;
    public Color darkTower;
    public Color poisonTower;
    public Color laserTower;
    public Color goldTower;
    public Color lifeTower;
    public Color deathTower;
    public GameObject projectile;
    public ColorSchemeScriptableObject[] availableColorSchemes;
    void Awake()
    {
        //setColorScheme(availableColorSchemes[0]);
        
        //doColorScheme(availableColorSchemes[0]);
        if (manager != null)
        {
            Destroy(gameObject);
            return;
        }
        doColorScheme(availableColorSchemes[0]);
        manager = this;
        TowerCode.projectile = projectile;
    }

    void setColorScheme(ColorSchemeScriptableObject scheme)
    {
        scheme.portal1 = portal1;
        scheme.portal1Highlighted = portal1Highlighted;
        scheme.portal2 = portal2;
        scheme.portal2Highlighted = portal2Highlighted;
        scheme.portal3 = portal3;
        scheme.portal3Highlighted = portal3Highlighted;
        scheme.wallPerm = wallPerm;
        scheme.wallPermHighlighted = wallPermHighlighted;
        scheme.tilePerm = tilePerm;
        scheme.tilePermHighlighted = tilePermHighlighted;
        scheme.pathPerm = pathPerm;
        scheme.pathPermHighlighted = pathPermHighlighted;
        scheme.start = start;
        scheme.startHighlighted = startHighlighted;
        scheme.end = end;
        scheme.endHighlighted = endHighlighted;
        scheme.checkpoint = checkpoint;
        scheme.checkpointHighlighted = checkpointHighlighted;
        scheme.tile = tile;
        scheme.tileHighlighted = tileHighlighted;
        scheme.wall = wall;
        scheme.wallHighlighted = wallHighlighted;
        scheme.path = path;
        scheme.pathHighlighted = pathHighlighted;
        scheme.background = background;
        scheme.towerHighlighted = towerHighlighted;
        scheme.tower = tower;
    }

    void doColorScheme(ColorSchemeScriptableObject scheme)
    {
        portal1 = scheme.portal1;
        portal1Highlighted = scheme.portal1Highlighted;
        portal2 = scheme.portal2;
        portal2Highlighted = scheme.portal2Highlighted;
        portal3 = scheme.portal3;
        portal3Highlighted = scheme.portal3Highlighted;
        wallPerm = scheme.wallPerm;
        wallPermHighlighted = scheme.wallPermHighlighted;
        tilePerm = scheme.tilePerm;
        tilePermHighlighted = scheme.tilePermHighlighted;
        pathPerm = scheme.pathPerm;
        pathPermHighlighted = scheme.pathPermHighlighted;
        start = scheme.start;
        startHighlighted = scheme.startHighlighted;
        end = scheme.end;
        endHighlighted = scheme.endHighlighted;
        checkpoint = scheme.checkpoint;
        checkpointHighlighted = scheme.checkpointHighlighted;
        tile = scheme.tile;
        tileHighlighted = scheme.tileHighlighted;
        wall = scheme.wall;
        wallHighlighted = scheme.wallHighlighted;
        path = scheme.path;
        pathHighlighted = scheme.pathHighlighted;
        tower = scheme.tower;
        towerHighlighted = scheme.towerHighlighted;
        background = scheme.background;
    }
}
