using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomicTower : TowerCode
{
    private new GameObject projectile;

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
                pc.code = new AtomicProjectile(i,controller.transform.position);
                pc.code.lvl = lvl;
                projectile.transform.position = controller.towerVisual.shoot();
                pc.material.color = getColor();
                pc.code.Start();
            }
        }
    }

    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == typeof(AtomicTower) && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return new AtomicProjectile(0,Vector3.zero);
    }

    public override Color getColor()
    {
        return ColorManager.manager.atomicTower;
    }
}
