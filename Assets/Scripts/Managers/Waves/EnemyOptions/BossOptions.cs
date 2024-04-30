using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BossOptions
{
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float vulnerabilityDecrease;
    public void config(FruitCode f)
    {
        ((BossCode)f).attackSpeed = attackSpeed;
        ((BossCode)f).vulnerabilityDecrease = vulnerabilityDecrease;
    }
}
