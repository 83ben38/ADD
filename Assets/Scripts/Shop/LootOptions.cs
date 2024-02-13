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
    public int[] items;
}
