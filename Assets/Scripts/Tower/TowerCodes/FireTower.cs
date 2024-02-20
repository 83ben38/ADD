using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : TowerCode
{
    public FireTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 2;
        attackSpeed = (int)(attackSpeed * (upgrade1 ? 1.15 : 1));
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    

    public override ProjectileCode create()
    {
        return new FireProjectile(upgrade1,upgrade2,upgrade3);
    }
    

    public override Color getColor()
    {
        return ColorManager.manager.fireTower;
    }
}
