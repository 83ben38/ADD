
using System;
using UnityEngine;
[Serializable]
public class WatermelonOptions
{
    [SerializeField]
    public int phase;
    [SerializeField]
    public GameObject splitObject;
    [SerializeField]
    public EnemyConfiguration splitConfig;
    //phase 1
    [SerializeField]
    public int damageReduction;
    // phase 1 + 4
    [SerializeField]
    public int healAmount;
    [SerializeField]
    public float healRadius;
    //phase 2
    [SerializeField]
    public float stunAmount;
    [SerializeField]
    public float stunRange;
    // phase 2 + 3
    [SerializeField]
    public int summon1Amount;
    [SerializeField]
    public int summon2Amount;
    [SerializeField]
    public GameObject summon1;
    [SerializeField]
    public GameObject summon2;
    [SerializeField]
    public EnemyConfiguration config1;
    [SerializeField]
    public EnemyConfiguration config2;
    // phase 4
    [SerializeField]
    public float speedAmount;
    public void config(WatermelonCode f)
    {
        f.phase = phase;
        f.splitObject = splitObject;
        f.splitConfig = splitConfig;
        f.damageReduction = damageReduction;
        f.healAmount = healAmount;
        f.healRadius = healRadius;
        f.stunAmount = stunAmount;
        f.stunRange = stunRange;
        f.summon1Amount = summon1Amount;
        f.summon2Amount = summon2Amount;
        f.summon1 = summon1;
        f.summon2 = summon2;
        f.config1 = config1;
        f.config2 = config2;
    }
}
