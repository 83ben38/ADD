using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : TowerCode
{

    public LightningTower()
    {
        range = 3;
        attackSpeed = 128;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == typeof(LightningTower) && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return new LightningProjectile();
    }

    public override Color getColor()
    {
        return ColorManager.manager.lightningTower;
    }
}
