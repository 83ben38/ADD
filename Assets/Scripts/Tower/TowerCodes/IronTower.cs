using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class IronTower : TowerCode
{
    public IronTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        
        range = 2;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

   

    public override ProjectileCode create()
    {
        return new IronProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override bool shoot()
    {
        List< List<TowerController>> path = PathfinderManager.manager.path;
        List<TowerController> availableTargets = new List<TowerController>();
        for (int i = 0; i < path.Count; i++)
        {
            for (int j = 0; j < path [i].Count ; j++)
            {


                TowerController possibleTarget = path[i][j];
                if ((possibleTarget.transform.position - controller.transform.position).magnitude <= (getRange() + 0.5f)*MapCreator.scale)
                {
                    availableTargets.Add(possibleTarget);
                }
            }
        }

        TowerController target = availableTargets[Random.Range(0,availableTargets.Count-1)];
        GameObject projectile = Object.Instantiate(TowerCode.projectile);
        ProjectileController pc = projectile.GetComponent<ProjectileController>();
        pc.code = create();
        pc.code.lvl = lvl;
        ((IronProjectile)pc.code).targetPath = target;
        projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
        pc.material.color = getColor();
        pc.code.Start(pc);
        return true;
    }

    public override Color getColor()
    {
        return ColorManager.manager.ironTower;
    }
}
