using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MapScriptableObject", order = 4)]
public class MapScriptableObject : ScriptableObject
{
    public int[] map;
    public int baseAward;
    public int xDimensions;
    public int yDimensions;
    public ShapeScriptableObject shape;
    public Vector3 startPos;
    public float scaleAmt;
    public int paths;
    public int checkpointsRequired;
}
