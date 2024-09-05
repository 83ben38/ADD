using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MangoOptions
{
    [SerializeField]
    public float radius;
    [SerializeField]
    public int healAmount;
    public void config(FruitCode f)
    {
        ((MangoCode)f).radius = radius;
        ((MangoCode)f).HealAmount = healAmount;
    }
}
