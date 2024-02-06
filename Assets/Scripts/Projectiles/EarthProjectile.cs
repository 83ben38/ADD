using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthProjectile : ProjectileCode
{
    public EarthProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3)
    {
        damage = 3;
        pierce = 10;
    }
}
