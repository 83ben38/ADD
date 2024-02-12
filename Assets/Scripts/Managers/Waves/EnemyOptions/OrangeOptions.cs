using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class OrangeOptions
{
    [SerializeField] 
    private GameObject childObject;
    [SerializeField]
    private EnemyConfiguration childConfig;
    [SerializeField] 
    private int splitAmount;
    public void config(FruitCode f)
    {
        OrangeCode o = (OrangeCode)f;
        o.splitAmount = splitAmount;
        o.splitObject = childObject;
        o.splitConfig = childConfig;
    }
}
