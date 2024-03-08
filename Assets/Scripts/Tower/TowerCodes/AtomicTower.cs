using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomicTower : TowerCode
{
    private GameObject[] projectiles;

    public AtomicTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        projectiles = new GameObject[] { null };
    }

    public override void MouseClick(TowerController controller)
    {
        if (upgrade1)
        {
            for (int i = 0; i < projectiles.Length; i++)
            {
                ((AtomicProjectile)projectiles[i].GetComponent<ProjectileController>().code).clockwise =
                    !((AtomicProjectile)projectiles[i].GetComponent<ProjectileController>().code).clockwise;
            }
        }
    }

    public override void tick()
    {
        
    }

    public override void roundStart()
    {
        projectiles = new GameObject[lvl];
        // create projectiles
        for (int i = 0; i < lvl; i++)
        {
            projectiles[i] = Object.Instantiate(projectile);
            ProjectileController pc = projectiles[i].GetComponent<ProjectileController>();
            pc.code = new AtomicProjectile(i,controller.transform.position,upgrade1,upgrade2,upgrade3);
            pc.code.lvl = lvl > 2 ? lvl : 2;
            projectiles[i].transform.position = controller.towerVisual.shoot(rechargeTime);
            pc.material.color = getColor();
            pc.code.Start(pc);
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

    public override object Clone()
    {
        return new AtomicTower(upgrade1, upgrade2, upgrade3);
    }
}
