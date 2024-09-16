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
    public BlueberryOptions blueberryOptions = null;
    [SerializeField] 
    public CoconutOptions coconutOptions = null;
    [SerializeField] 
    public LemonOptions lemonOptions = null;
    [SerializeField] 
    public OrangeOptions orangeOptions = null;
    [SerializeField] 
    public MangoOptions mangoOptions = null;
    [SerializeField] 
    public BananaOptions bananaOptions = null;
    [SerializeField] 
    public StrawberryOptions strawberryOptions = null;
    [SerializeField] 
    public GrapeOptions grapeOptions = null;
    [SerializeField] 
    public BossOptions bossOptions = null;
   
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
        if (fruitCode is MangoCode)
        {
            mangoOptions.config(fruitCode);
        }

        if (fruitCode is BananaCode)
        {
            bananaOptions.config(fruitCode);
        }

        if (fruitCode is StrawberryCode)
        {
            strawberryOptions.config(fruitCode);
        }

        if (fruitCode is GrapeCode)
        {
            grapeOptions.config(fruitCode);
        }
        if (fruitCode is BossCode)
        {
            bossOptions.config(fruitCode);
        }

        if (fruitCode is WatermelonCode w)
        {
            BossConfigManager.manager.config(w);
        }
    }
}
