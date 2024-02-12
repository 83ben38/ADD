using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BlueberryOptions
{
    [SerializeField]
    private int speedIncrease;
    public void config(FruitCode f)
    {
        ((BlueberryCode)f).speedIncrease = speedIncrease/1000f;
    }
}
