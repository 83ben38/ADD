using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BananaOptions
{
    [SerializeField]
    private int speedIncrease;
    public void config(FruitCode f)
    {
        ((BananaCode)f).speedIncrease = speedIncrease;
    }
}
