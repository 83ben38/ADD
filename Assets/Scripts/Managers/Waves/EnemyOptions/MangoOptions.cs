using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MangoOptions
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private int healAmount;
    public void config(FruitCode f)
    {
        ((MangoCode)f).radius = radius;
        ((MangoCode)f).HealAmount = healAmount;
    }
}
