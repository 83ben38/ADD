using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class EnemyConfiguration
{
    [SerializeField]
    public int hp;
    [SerializeField]
    public int speed;
    [SerializeField]
    public int maxSize;
    [SerializeField]
    public int minSize;
    [SerializeField] 
    private BlueberryOptions blueberryOptions = null;
    [SerializeField] 
    private CoconutOptions coconutOptions = null;
    [SerializeField] 
    private LemonOptions lemonOptions = null;
    [SerializeField] 
    private OrangeOptions orangeOptions = null;
    public void runOptions(FruitCode fruitCode)
    {
        if (fruitCode is BlueberryCode)
        {
            blueberryOptions.config(fruitCode);
        }
        if (fruitCode is CoconutCode)
        {
            coconutOptions.config(fruitCode);
        }
        if (fruitCode is LemonCode)
        {
            lemonOptions.config(fruitCode);
        }
        if (fruitCode is OrangeCode)
        {
            orangeOptions.config(fruitCode);
        }
    }
}
