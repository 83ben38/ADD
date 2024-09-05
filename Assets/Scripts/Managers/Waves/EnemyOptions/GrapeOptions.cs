using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GrapeOptions
{
    [SerializeField] 
    public GameObject childObject;
    [SerializeField]
    public EnemyConfiguration childConfig;
    [SerializeField] 
    public float spawnCooldown;
    public void config(FruitCode f)
    {
        GrapeCode o = (GrapeCode)f;
        o.spawnCooldown = spawnCooldown;
        o.spawnObject = childObject;
        o.spawnConfig = childConfig;
    }
}
