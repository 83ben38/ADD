using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerDescriptionScriptableObject", order = 6)]
public class TowerDescriptionScriptableObject : ScriptableObject
{
    public string title;
    [TextArea(5,10)]
    public string description;
    [TextArea(5,10)]
    public string upgrade1Description, upgrade2Description,upgrade3Description;
}
