using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LemonOptions
{
    [SerializeField]
    public float radius;
    [SerializeField]
    public int speedIncrease;
    public void config(FruitCode f)
    {
        ((LemonCode)f).radius = radius;
        ((LemonCode)f).speedAmount = speedIncrease/1000f;
    }
}
