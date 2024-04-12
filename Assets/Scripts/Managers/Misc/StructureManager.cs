using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{

    public static StructureManager manager;
    [SerializeField]
    private StructureScriptableObject[] availableStructures;

    private List<List<StructureScriptableObject>> sortedStructures;

    private void Start()
    {
        manager = this;
        sortedStructures = new List<List<StructureScriptableObject>>();
        foreach (StructureScriptableObject sso in availableStructures)
        {
            while (sso.centerTower >= sortedStructures.Count)
            {
                sortedStructures.Add(new List<StructureScriptableObject>());
            }
            sortedStructures[sso.centerTower].Add(sso);
        }
    }

    public List<StructureScriptableObject> getPotentialStructures(int towerNum)
    {
        if (towerNum >= sortedStructures.Count) return new List<StructureScriptableObject>();
        return sortedStructures[towerNum];
    }
}
