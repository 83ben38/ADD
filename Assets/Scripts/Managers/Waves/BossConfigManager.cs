using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossConfigManager : MonoBehaviour
{
    public static BossConfigManager manager;
    
    public GameObject waterMelon2;
    public GameObject waterMelon3;
    public GameObject waterMelon4;
    public EnemyConfiguration waterMelon2Config;
    public EnemyConfiguration waterMelon3Config;
    public EnemyConfiguration waterMelon4Config;
    public int damageReduction;
    public int phase1HealAmount;
    public float stunAmount;
    public float stunRange;
    public int summon11Amount;
    public GameObject summon11;
    public EnemyConfiguration summon11Config;
    public int summon12Amount;
    public GameObject summon12;
    public EnemyConfiguration summon12Config;
    public int summon21Amount;
    public GameObject summon21;
    public EnemyConfiguration summon21Config;
    public int summon22Amount;
    public GameObject summon22;
    public EnemyConfiguration summon22Config;
    public int phase4HealAmount;
    public float healRadius;
    public float speedAmount;
    void Start()
    {
        manager = this;
    }

    public void config(WatermelonCode w)
    {
        if (w.phase == 1)
        {
            w.splitObject = waterMelon2;
            w.splitConfig = waterMelon2Config;
            w.damageReduction = damageReduction;
            w.healAmount = phase1HealAmount;
        }

        if (w.phase == 2)
        {
            w.splitConfig = waterMelon3Config;
            w.splitObject = waterMelon3;
            w.summon1 = summon11;
            w.summon1Amount = summon11Amount;
            w.config1 = summon11Config;
            w.summon2 = summon12;
            w.summon2Amount = summon12Amount;
            w.config2 = summon12Config;
            w.stunAmount = stunAmount;
            w.stunRange = stunRange;
        }

        if (w.phase == 3)
        {
            w.splitConfig = waterMelon4Config;
            w.splitObject = waterMelon4;
            w.summon1 = summon21;
            w.summon1Amount = summon21Amount;
            w.config1 = summon21Config;
            w.summon2 = summon22;
            w.summon2Amount = summon22Amount;
            w.config2 = summon22Config;
        }

        if (w.phase == 4)
        {
            w.healAmount = phase4HealAmount;
            w.healRadius = healRadius;
            w.speedAmount = speedAmount;
        }
        
    }
}
