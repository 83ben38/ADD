using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkProjectile : ProjectileCode
{
    

    public DarkProjectile(bool upgrade1, bool upgrade2, bool upgrade3, int statIncrease) : base(upgrade1, upgrade2, upgrade3)
    {
        damage = 1 + (int)Math.Sqrt(statIncrease);
        pierce = 1 + (int)Math.Sqrt(statIncrease);
    }
}
