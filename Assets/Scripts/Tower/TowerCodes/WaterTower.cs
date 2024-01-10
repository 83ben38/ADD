using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTower : TowerCode
{
    public WaterTower()
    {
        range = 2;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == typeof(WaterTower) && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return null;
    }

    public override Color getColor()
    {
        return ColorManager.manager.waterTower;
    }
}
