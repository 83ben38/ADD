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
    public BlueberryOptions blueberryOptions = new BlueberryOptions();
    [SerializeField] 
    public CoconutOptions coconutOptions = new CoconutOptions();
    [SerializeField] 
    public LemonOptions lemonOptions = new LemonOptions();
    [SerializeField] 
    public OrangeOptions orangeOptions = new OrangeOptions();
    [SerializeField] 
    public MangoOptions mangoOptions = new MangoOptions();
    [SerializeField] 
    public BananaOptions bananaOptions = new BananaOptions();
    [SerializeField] 
    public StrawberryOptions strawberryOptions = new StrawberryOptions();
    [SerializeField] 
    public GrapeOptions grapeOptions = new GrapeOptions();
    [SerializeField] 
    public BossOptions bossOptions = new BossOptions();
    [SerializeField] 
    public WatermelonOptions watermelonOptions = new WatermelonOptions();
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

        if (fruitCode is WatermelonCode code)
        {
            watermelonOptions.config(code);
        }
    }
}
