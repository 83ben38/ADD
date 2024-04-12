using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StructurreScriptableObject", order = 10)]
public class StructureScriptableObject : ScriptableObject
{
    public int centerTower;
    public int[] triangleConfig;
    public int[] squareConfig;
    public int[] pentagonConfig;
    public int[] hexagonConfig;
    public int[] triangleLevels;
    public int[] squareLevels;
    public int[] pentagonLevels;
    public int[] hexagonLevels;
    public int towerMade;
    public int resultingLevel;

    public TowerCode makeTower()
    {
        TowerCode tc = TowerCodeFactory.getTowerCode(towerMade);
        tc.lvl = resultingLevel;
        return tc;
    }

    public int[] getConfig()
    {
        switch (PathfinderManager.manager.shapeCode)
        {
            case "hexagon" : return hexagonConfig;
            case "cube" : return squareConfig;
            case "triangle" : return triangleConfig;
            case "pentagon" : return pentagonConfig;
        }

        return null;
    }
    public int[] getLevels()
    {
        switch (PathfinderManager.manager.shapeCode)
        {
            case "hexagon" : return hexagonLevels;
            case "cube" : return squareLevels;
            case "triangle" : return triangleLevels;
            case "pentagon" : return pentagonLevels;
        }

        return null;
    }
}
