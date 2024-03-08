using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertiaTower : TowerCode
{
    private float accelerationFactor = 64f;
    public InertiaTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 2;
        attackSpeed = 256;
    }
    public override void tick()
    {
        if (ticksLeft > 0)
        {
            ticksLeft -= Time.deltaTime*accelerationFactor;
            accelerationFactor += 8 * Time.deltaTime * lvl;
        }

        if (ticksLeft <= 0)
        {
            
            if (shoot())
            {
                ticksLeft = getAttackSpeed() + ticksLeft;
                rechargeTime = (getAttackSpeed() - 1)*(64f/accelerationFactor);
            }
        }
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override ProjectileCode create()
    {
        return new InertiaProjectile(upgrade1, upgrade2, upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.inertiaTower;
    }

    public override void roundStart()
    {
        accelerationFactor = 64f;
    }

    public override object Clone()
    {
        return new InertiaTower(upgrade1, upgrade2, upgrade3);
    }
}
