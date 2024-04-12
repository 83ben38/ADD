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
    public int towerMade;
}
