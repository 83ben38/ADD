using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTower : TowerCode
{
    public WaterTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 2;
        attackSpeed = (int)(attackSpeed * (upgrade1 ? 2.3 : 2));
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    

    public override ProjectileCode create()
    {
        return new WaterProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.waterTower;
    }

    public override object Clone()
    {
        return new WaterTower(upgrade1, upgrade2, upgrade3);
    }
}
