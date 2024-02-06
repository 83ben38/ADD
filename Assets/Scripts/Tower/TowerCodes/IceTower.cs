using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : TowerCode
{
    public IceTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 1;
        
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == typeof(IceTower) && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return new IceProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.iceTower;
    }
}
