using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomicTower : TowerCode
{
    private List<GameObject> projectiles;

    public AtomicTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        projectiles = new List<GameObject>();
    }

    public override void MouseClick(TowerController controller)
    {
        
        if (upgrade1)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                ((AtomicProjectile)projectiles[i].GetComponent<ProjectileController>().code).clockwise =
                    !((AtomicProjectile)projectiles[i].GetComponent<ProjectileController>().code).clockwise;
            }
        }
    }

    public override void tick()
    {
        if (upgrade3)
        {
            base.tick();
        }
    }

    public override bool shoot()
    {
        GameObject projectile = Object.Instantiate(TowerCode.projectile);
        ProjectileController pc = projectile.GetComponent<ProjectileController>();
        pc.code = new AtomicProjectile(0,controller.transform.position,upgrade1,upgrade2,upgrade3);
        pc.code.lvl = lvl > 1 ? lvl : 2;
        projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
        pc.material.color = getColor();
        pc.code.Start(pc);
        projectiles.Add(projectile);
        if (upgrade2)
        {
            for (int j = 0; j < (lvl > 2 ? lvl : 2); j++)
            {
                GameObject go = Object.Instantiate(projectile);
                ProjectileController pc1 = go.GetComponent<ProjectileController>();
                pc1.code = new AtomicProjectile(j, (AtomicProjectile)pc.code, upgrade1, upgrade2, upgrade3);
                pc1.code.lvl = lvl > 2 ? lvl : 2;
                go.transform.position = projectile.transform.position;
                go.transform.localScale *= 0.5f;
                pc1.material.color = getColor();
                pc1.code.Start(pc1);
            }
        }
        return true;
    }

    public override void roundStart()
    {
        base.roundStart();
        projectiles = new List<GameObject>();
        if (!upgrade3)
        {
            projectiles = new List<GameObject>(lvl);
            // create projectiles
            for (int i = 0; i < lvl; i++)
            {
                projectiles.Add(Object.Instantiate(projectile));
                ProjectileController pc = projectiles[i].GetComponent<ProjectileController>();
                pc.code = new AtomicProjectile(i, controller.transform.position, upgrade1, upgrade2, upgrade3);
                pc.code.lvl = lvl > 2 ? lvl : 2;
                projectiles[i].transform.position = controller.towerVisual.shoot(rechargeTime);
                pc.material.color = getColor();
                pc.code.Start(pc);
                if (upgrade2)
                {
                    for (int j = 0; j < (lvl > 2 ? lvl : 2); j++)
                    {
                        GameObject go = Object.Instantiate(projectile);
                        ProjectileController pc1 = go.GetComponent<ProjectileController>();
                        pc1.code = new AtomicProjectile(j, (AtomicProjectile)pc.code, upgrade1, upgrade2, upgrade3);
                        pc1.code.lvl = lvl > 2 ? lvl : 2;
                        go.transform.position = projectiles[i].transform.position;
                        go.transform.localScale *= 0.5f;
                        pc1.material.color = getColor();
                        pc1.code.Start(pc1);
                    }
                }
            }
        }

    }


    public override ProjectileCode create()
    {
        return new AtomicProjectile(0,controller.transform.position,upgrade1,upgrade2,upgrade3);
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
