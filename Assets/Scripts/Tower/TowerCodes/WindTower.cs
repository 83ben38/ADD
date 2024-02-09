using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTower : TowerCode
{


    public WindTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3) {
        range = 1;
        attackSpeed = 32;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override int getAttackSpeed()
    {
        return 32 * lvl;
    }

    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == typeof(WindTower) && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return new WindProjectile(upgrade1,upgrade2,upgrade3,controller) ;
    }

    public override Color getColor()
    {
       return ColorManager.manager.WindTower;
    }

    public override bool shoot()
    {
        List<TowerController> nextTo = controller.nextTo;
        if (getRange() > 1)
        {
            int z = nextTo.Count;
            for (int i = 0; i < z; i++)
            {

                List<TowerController> nextToNextTo = nextTo[i].nextTo;

                for (int j = 0; j < nextToNextTo.Count; j++)
                {
                    if (! nextTo.Contains(nextToNextTo[j]) )
                    {
                            
                        nextTo.Add(nextToNextTo[j]);
                            
                    } 
                }
            }
        }
        for (int i = 0; i < nextTo.Count; i++)
        {
            if (nextTo[i].tower != null && !(nextTo[i].tower is WindTower))
            {
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = new WindProjectile(upgrade1,upgrade2,upgrade3,nextTo[i]);
                pc.code.lvl = lvl;
                projectile.transform.position = controller.towerVisual.shoot();
                pc.material.color = getColor();
                pc.code.Start(pc);
            }
        }
        return true;
    }

    public override void tick()
    {
        base.tick();

        List<TowerController> nextTo = controller.nextTo;
            if (getRange() > 1)
            {
                int z = nextTo.Count;
                for (int i = 0; i < z; i++)
                {

                    List<TowerController> nextToNextTo = nextTo[i].nextTo;

                    for (int j = 0; j < nextToNextTo.Count; j++)
                    {
                        if (! nextTo.Contains(nextToNextTo[j]) )
                        {
                            
                            nextTo.Add(nextToNextTo[j]);
                            
                        } 
                    }
                }
            }
        

        for (int i = 0; i < nextTo.Count; i++)
        {
            if (nextTo[i].tower != null && !(nextTo[i].tower is WindTower))
            {
                nextTo[i].tower.ticksLeft -= Time.deltaTime * nextTo[i].tower.lvl * lvl * 11f;
            }
        }



    }
}
