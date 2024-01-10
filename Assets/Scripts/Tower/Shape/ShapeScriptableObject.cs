using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShapeScriptableObject", order = 1)]
public class ShapeScriptableObject : ScriptableObject
{
    [Header("Mesh")] 
    public Mesh mesh;

    public Vector3 meshPosition;
    public Quaternion meshRotation;
    public Vector3 meshScale;

    [Header("Positioning (first letter is position, second letter is coordinate)")]
    public float xXFactor;
    public float xYFactor;
    public float yXFactor;
    public float yYFactor;
    public int xYFactorMod;
    public int yXFactorMod;
    [Header("Next To Check")] 
    public string checkName;

    public Vector3[] dotPositions;
}