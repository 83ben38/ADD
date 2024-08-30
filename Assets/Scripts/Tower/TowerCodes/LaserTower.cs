using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class LaserTower : TowerCode
{
    public List<ProjectileController> projectiles;
    public static List<LaserTower> priority = new List<LaserTower>();
    public LaserTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        projectiles = new List<ProjectileController>();
        range = 2;
    }
    
    public override void MouseClick(TowerController controller)
    {
        
    }

    public override float getRange()
    {
        if (upgrade1)
        {
            return base.getRange()+1;
        }
        
        return lvl + range;
    }

    public override void tick()
    {
        
    }

    public override void roundStart()
    {
        base.roundStart();
        if (priority[0] == this)
        {
            projectiles.RemoveAll(item => item == null);
            for (int i = 0; i < priority.Count; i++)
            { 
                priority[i].projectiles.RemoveAll(item => item == null);
            }

            for (int i = 0; i < priority.Count; i++)
            {
                priority[i].runLasers();
            }
        }
    }

    public void runLasers()
    {
        List<TowerController> nextTo;
        if (controller == null)
        {
            return;
        }
        else
        {
            nextTo = new List<TowerController>(controller.nextTo);
        }

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
                        if (!nextTo.Contains(nextToNextTo[j]) )
                        {
                            nextTo.Add(nextToNextTo[j]);
                            nextToNextTo[j].minDist = k+1;
                        } 
                    }
                }
            }

            foreach (var laserTower in priority)
            {
                if (nextTo.Contains(laserTower.controller))
                {
                    TowerController t = laserTower.controller;
                    if (t.tower == null || t.tower is not LaserTower || t.tower.getRange() < t.minDist ||
                        t == controller)
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
                            if (lp.end == t && lp.start == controller)
                            {
                                goto end;
                            }
                        }

                        LaserTower lt = ((LaserTower)t.tower);
                        if (projectiles.Count == maxLasers() || lt.projectiles.Count == lt.maxLasers())
                        {
                            continue;
                        }

                        GameObject projectile = Object.Instantiate(TowerCode.projectile);
                        ProjectileController pc = projectile.GetComponent<ProjectileController>();
                        pc.code = new LaserProjectile(upgrade1, upgrade2, upgrade3, controller, t);
                        pc.code.lvl = lvl + t.tower.lvl;
                        pc.material.color = getColor();
                        pc.code.Start(pc);
                        projectiles.Add(pc);
                        lt.projectiles.Add(pc);
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

    public override object Clone()
    {
        return new LaserTower(upgrade1, upgrade2, upgrade3);
    }

    public override void placedDown(TowerController tc)
    {
        base.placedDown(tc);
        for (int i = 0; i < priority.Count; i++)
        {
            if (priority[i].lvl <= lvl)
            {
                priority.Insert(i,this);
                return;
            }
        }
        priority.Add(this);
    }

    public override void pickedUp()
    {
        base.pickedUp();
        priority.Remove(this);
    }

    public int maxLasers()
    {
        if (upgrade1)
        {
            return lvl > 4 ? 3 : 2;
        }

        switch (lvl)
        {
            case 1: return 2;
            case 2: return 2;
            case 3: return 3;
            case 4: return 4;
            case 5: return 6;
            case 6: return 10;
            case 7: return 18;
        }

        return 0;
    }

}
