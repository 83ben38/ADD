using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            if (SaveData.save.getAvailableTowers().Contains(sso.towerMade))
            {
                sortedStructures[sso.centerTower].Add(sso);
            }

            
        }
    }

    public List<StructureScriptableObject> getPotentialStructures(int towerNum)
    {
        if (towerNum >= sortedStructures.Count || towerNum < 0) return new List<StructureScriptableObject>();
        return sortedStructures[towerNum];
    }
}
