using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTower : TowerCode
{
   

    public SnowTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 0.5f;
        attackSpeed = 128;
    }

    public override float getRange()
    {
        return (range*2) + (lvl > 4 ? 1.0f : 0.0f);
    }
    
    public override void tick()
    {
        if (ticksLeft > 0)
        {
            ticksLeft -= lvl*Time.deltaTime*32f;
        }

        if (ticksLeft <= 0)
        {
            
            if (shoot())
            {
                ticksLeft = getAttackSpeed() + ticksLeft;
                rechargeTime = getAttackSpeed() - 1;
            }
        }
    }
    public override void MouseClick(TowerController controller)
    {
        
    }

    public override ProjectileCode create()
    {
        return new SnowProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.snowTower;
    }

    public override object Clone()
    {
        return new SnowTower(upgrade1, upgrade2, upgrade3);
    }
}
