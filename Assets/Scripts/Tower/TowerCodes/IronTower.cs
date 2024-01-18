using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class IronTower : TowerCode
{
    public IronTower()
    {
        range = 2;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == typeof(IronTower) && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return new IronProjectile();
    }

    public override bool shoot()
    {
        List<TowerController> path = PathfinderManager.manager.path;
        List<TowerController> availableTargets = new List<TowerController>();
        for (int i = 0; i < path.Count; i++)
        {
            TowerController possibleTarget = path[i];
            if ((possibleTarget.transform.position - controller.transform.position).magnitude <= getRange() + 0.5f)
            {
                availableTargets.Add(possibleTarget);
            }
        }

        TowerController target = availableTargets[Random.Range(0,availableTargets.Count-1)];
        GameObject projectile = Object.Instantiate(TowerCode.projectile);
        ProjectileController pc = projectile.GetComponent<ProjectileController>();
        pc.code = create();
        pc.code.lvl = lvl;
        ((IronProjectile)pc.code).targetPath = target;
        projectile.transform.position = controller.towerVisual.shoot();
        pc.material.color = getColor();
        pc.code.Start();
        return true;
    }

    public override Color getColor()
    {
        return ColorManager.manager.ironTower;
    }
}