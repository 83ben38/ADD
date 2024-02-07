using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LoadoutScriptableObject", order = 6)]
public class LoadoutScriptableObject : ScriptableObject
{
    public int[] loadout;
}
