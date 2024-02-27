using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StrawberryOptions
{
    [SerializeField]
    private float stunRadius;
    [SerializeField] 
    private float stunAmount;
    public void config(FruitCode f)
    {
        ((StrawberryCode)f).stunRadius = stunRadius;
        ((StrawberryCode)f).stunAmount = stunAmount * 64f;
    }
}
