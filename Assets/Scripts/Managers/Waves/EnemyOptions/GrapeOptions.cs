using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GrapeOptions
{
    [SerializeField] 
    private GameObject childObject;
    [SerializeField]
    private EnemyConfiguration childConfig;
    [SerializeField] 
    private float spawnCooldown;
    public void config(FruitCode f)
    {
        GrapeCode o = (GrapeCode)f;
        o.spawnCooldown = spawnCooldown;
        o.spawnObject = childObject;
        o.spawnConfig = childConfig;
    }
}
