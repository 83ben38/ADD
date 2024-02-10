using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTower : TowerCode
{
    public ColorTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        attackSpeed = 192;
        range = 2;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool canMerge(TowerCode c)
    {
        return c.lvl == lvl;
    }

    public override TowerCode merge(TowerCode c)
    {
        c.lvl++;
        return c;
    }

    public override ProjectileCode create()
    {
        return new ColorProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return new Color(1, 1, 1, 1);
    }
}
