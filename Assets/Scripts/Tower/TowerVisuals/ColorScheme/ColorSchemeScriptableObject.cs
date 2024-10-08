using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ColorSchemeScriptableObject", order = 10)]
public class ColorSchemeScriptableObject : ScriptableObject
{
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
    public Color tower;
    public Color towerHighlighted;
    public Color structure;
    public Color structureHighlighted;
    public Color background;
}
