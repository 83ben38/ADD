using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class OrangeOptions
{
    [SerializeField] 
    public GameObject childObject;
    [SerializeField]
    public EnemyConfiguration childConfig;
    [SerializeField] 
    public int splitAmount;
    public void config(FruitCode f)
    {
        OrangeCode o = (OrangeCode)f;
        o.splitAmount = splitAmount;
        o.splitObject = childObject;
        o.splitConfig = childConfig;
    }
}
