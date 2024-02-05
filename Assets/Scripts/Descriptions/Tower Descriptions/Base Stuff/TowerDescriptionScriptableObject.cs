using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerDescriptionScriptableObject", order = 5)]
public class TowerDescriptionScriptableObject : ScriptableObject
{
    public string title;
    public string description;
}
