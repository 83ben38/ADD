using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LootOptions
{
    [SerializeField]
    public float chance;
    [SerializeField] 
    public List<int> items;

    public LootOptions(LootOptions lo)
    {
        items = new List<int>(lo.items);
        chance = lo.chance;
    }
}
