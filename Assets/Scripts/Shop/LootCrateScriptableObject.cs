using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LootCrateScriptableObject", order = 7)]
public class LootCrateScriptableObject : ScriptableObject
{
    public LootOptions[] chances;
    public bool loadouts;
    public int cost;
}
