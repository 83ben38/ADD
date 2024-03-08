using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertiaProjectile : ProjectileCode
{
    public InertiaProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        pierce = 1;
    }

    public override int getDamage()
    {
        return new int[] { 1, 2, 4, 8, 16, 32, 64 }[lvl];
    }
}
