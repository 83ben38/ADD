using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LootCrateScriptableObject", order = 7)]
public class LootCrateScriptableObject : ScriptableObject
{
    public List<LootOptions> chances;
    public bool loadouts;
    public int cost;

    public LootCrateScriptableObject(LootCrateScriptableObject lcso)
    {
        chances = new List<LootOptions>();
        loadouts = lcso.loadouts;
        cost = lcso.cost;
        for (int i = 0; i < lcso.chances.Count; i++)
        {
            chances.Add(new LootOptions(lcso.chances[i]));   
        }
    }

    public string crateName;
}
