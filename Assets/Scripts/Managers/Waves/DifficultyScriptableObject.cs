using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DifficultyScriptableObject", order = 3)]
public class DifficultyScriptableObject : ScriptableObject
{
    public WaveScriptableObject[] waves;
    public float freeplayCreditMultiplier;
    public int maxLives;
    public int awardMultiplier;
    public string difficultyName;
    public Color difficultyColor;
    public Color difficultyColorHighlighted;
    public int[] preRequisites;
}
