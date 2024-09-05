using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CoconutOptions
{
    [SerializeField]
    public int reductionMod;
    public void config(FruitCode f)
    {
        ((CoconutCode)f).reductionMod = reductionMod;
    }
}
