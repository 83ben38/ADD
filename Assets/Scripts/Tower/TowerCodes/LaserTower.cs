using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTower : TowerCode
{
    public List<ProjectileController> projectiles;
    public LaserTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        projectiles = new List<ProjectileController>();
    }
    
    public override void MouseClick(TowerController controller)
    {
        
    }

    public override float getRange()
    {
        return lvl + 2;
    }

    public override void tick()
    {
        int count = projectiles.Count;
        projectiles.RemoveAll(item => item == null);
        if (projectiles.Count == 0 || projectiles.Count != count)
        {
            List<TowerController> nextTo = new List<TowerController>(controller.nextTo);
            for (int i = 0; i < nextTo.Count; i++)
            {
                nextTo[i].minDist = 1;
            }
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
                            nextToNextTo[j].minDist = k+1;
                        } 
                    }
                }
            }

            foreach (var t in nextTo)
            {
                if (t.tower == null || t.tower is not LaserTower || t.tower.getRange() < t.minDist || t == controller)
                {
                    continue;
                }
                else
                {
                    for (int i = 0; i < projectiles.Count; i++)
                    {
                        LaserProjectile lp = ((LaserProjectile)projectiles[i].code);
                        if (lp.start == t && lp.end == controller)
                        {
                            goto end;
                        }
                    }
                    GameObject projectile = Object.Instantiate(TowerCode.projectile);
                    ProjectileController pc = projectile.GetComponent<ProjectileController>();
                    pc.code = new LaserProjectile(upgrade1, upgrade2, upgrade3, controller, t);
                    pc.code.lvl = lvl+t.tower.lvl;
                    pc.material.color = getColor();
                    pc.code.Start(pc);
                    projectiles.Add(pc);
                    ((LaserTower)t.tower).projectiles.Add(pc);
                }
                end: ;
            }
            
        }
    }

    public override ProjectileCode create()
    {
        return new LaserProjectile(upgrade1,upgrade2,upgrade3,controller,controller);
    }

    public override Color getColor()
    {
        return ColorManager.manager.laserTower;
    }
}
