using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LemonOptions
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private int speedIncrease;
    public void config(FruitCode f)
    {
        ((LemonCode)f).radius = radius;
        ((LemonCode)f).speedAmount = speedIncrease/1000f;
    }
}
