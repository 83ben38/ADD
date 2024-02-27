using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class FreeplayEnemyConfiguration
{
    [SerializeField]
    public int defaultHp;
    [SerializeField] 
    public float hpScaling;
    [SerializeField]
    public int defaultSpeed;
    [SerializeField]
    public float speedScaling;
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
    [SerializeField] 
    private MangoOptions mangoOptions = null;
    [SerializeField] 
    private BananaOptions bananaOptions = null;
    [SerializeField] 
    private StrawberryOptions strawberryOptions = null;
    [SerializeField] 
    private GrapeOptions grapeOptions = null;
    public void runOptions(FruitCode fc, int increase)
    {
        fc.hp = defaultHp + (int)(hpScaling*increase);
        fc.maxHp = defaultHp + (int)(hpScaling*increase);
        fc.speed = (defaultSpeed + (speedScaling*increase))/1000f;
        fc.minScale = minSize/1000f;
        fc.maxScale = maxSize/1000f;
        fc.transform.localScale = new Vector3(fc.maxScale, fc.maxScale, fc.maxScale)*MapCreator.scale;
        if (fc is BlueberryCode)
        {
            blueberryOptions.config(fc);
        }
        if (fc is CoconutCode)
        {
            coconutOptions.config(fc);
        }
        if (fc is LemonCode)
        {
            lemonOptions.config(fc);
        }
        if (fc is OrangeCode)
        {
            orangeOptions.config(fc);
        }
        if (fc is MangoCode)
        {
            mangoOptions.config(fc);
        }

        if (fc is BananaCode)
        {
            bananaOptions.config(fc);
        }

        if (fc is StrawberryCode)
        {
            strawberryOptions.config(fc);
        }

        if (fc is GrapeCode)
        {
            grapeOptions.config(fc);
        }
    }
}
