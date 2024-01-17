using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : TowerCode
{
    public FireTower()
    {
        range = 2;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == typeof(FireTower) && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return new FireProjectile();
    }
    

    public override Color getColor()
    {
        return ColorManager.manager.fireTower;
    }
}
