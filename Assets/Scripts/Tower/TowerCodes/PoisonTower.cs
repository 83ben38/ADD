using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : TowerCode
{
    public PoisonTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 1;
        attackSpeed *= 2;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    
    

    public override ProjectileCode create()
    {
        return new PoisonProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.poisonTower;
    }

    public override object Clone()
    {
        return new PoisonTower(upgrade1, upgrade2, upgrade3);
    }
}
