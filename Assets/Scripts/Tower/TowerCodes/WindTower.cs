using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTower : TowerCode
{
    private List<TowerController> buffed = new List<TowerController>();
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

    

    public override ProjectileCode create()
    {
        return new WindProjectile(upgrade1,upgrade2,upgrade3,controller) ;
    }

    public override Color getColor()
    {
       return ColorManager.manager.windTower;
    }

    public override object Clone()
    {
        return new WindTower(upgrade1, upgrade2, upgrade3);
    }

    public override bool shoot()
    {
        
        for (int i = 0; i < buffed.Count; i++)
        {
            if (buffed[i].tower != null && !(buffed[i].tower is WindTower))
            {
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = new WindProjectile(upgrade1,upgrade2,upgrade3,buffed[i]);
                pc.code.lvl = lvl > 1 ? lvl : 2;
                projectile.transform.position = controller.towerVisual.shoot(getAttackSpeed()-1);
                pc.material.color = getColor();
                pc.code.Start(pc);
            }
        }

        buffed = new List<TowerController>();
        return true;
    }

    public override void tick()
    {
        base.tick();

        List<TowerController> nextTo = new List<TowerController>(controller.nextTo);
        for (int k = 0; k < getRange()-1; k++)
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
            if (nextTo[i].tower != null && !(nextTo[i].tower is WindTower) && nextTo[i].tower.ticksLeft > 0)
            {
                nextTo[i].tower.ticksLeft -= Time.deltaTime * lvl * 11f;
                nextTo[i].tower.rechargeTime -= Time.deltaTime * lvl * 11f;
                if (!buffed.Contains(nextTo[i]))
                {
                    buffed.Add(nextTo[i]);
                }
            }
        }



    }
}
