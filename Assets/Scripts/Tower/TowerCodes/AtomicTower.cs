using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomicTower : TowerCode
{
    private new GameObject projectile;

    public AtomicTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override void tick()
    {
        if (projectile == null)
        {
            // create projectiles
            for (int i = 0; i < lvl; i++)
            {
                projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = new AtomicProjectile(i,controller.transform.position,upgrade1,upgrade2,upgrade3);
                pc.code.lvl = lvl;
                projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
                pc.material.color = getColor();
                pc.code.Start(pc);
            }
        }
    }

    

    public override ProjectileCode create()
    {
        return new AtomicProjectile(0,Vector3.zero,upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.atomicTower;
    }
}
