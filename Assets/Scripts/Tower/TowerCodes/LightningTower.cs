using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : TowerCode
{

    public LightningTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 3;
        attackSpeed = 128;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    

    public override ProjectileCode create()
    {
        return new LightningProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.lightningTower;
    }
}
