using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTower : TowerCode
{

    public EarthTower()
    {
        
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == typeof(EarthTower) && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return new EarthProjectile();
    }

    public override Color getColor()
    {
        return ColorManager.manager.earthTower;
    }
}
