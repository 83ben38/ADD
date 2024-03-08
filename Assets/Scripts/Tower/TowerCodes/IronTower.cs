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
        attackSpeed *= upgrade1 ? 16 : 1;
    }

    public override float getRange()
    {
        return upgrade1 ? -1.5f : base.getRange();
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override int getAttackSpeed()
    {
        switch (lvl)
        {
            case 5: return (int)(base.getAttackSpeed() * .75);
            case 6: return (int)(base.getAttackSpeed() * .55);
            case 7: return (int)(base.getAttackSpeed() * .38);
        }
        return base.getAttackSpeed();
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
                if ((possibleTarget.transform.position - controller.transform.position).magnitude <= (getRange() + 0.5f)*MapCreator.scale || upgrade1)
                {
                    availableTargets.Add(possibleTarget);
                }
            }
        }

        if (availableTargets.Count == 0)
        {
            return true;
        }

        if (upgrade1)
        {
            for (int i = 0; i < availableTargets.Count; i++)
            {
                TowerController t = availableTargets[i];
                GameObject pr = Object.Instantiate(TowerCode.projectile);
                ProjectileController pco = pr.GetComponent<ProjectileController>();
                pco.code = create();
                pco.code.lvl = lvl > 1 ? lvl : 2;
                ((IronProjectile)pco.code).targetPath = t;
                pr.transform.position = controller.towerVisual.shoot(rechargeTime);
                pco.material.color = getColor();
                pco.code.Start(pco);
            }
            return true;
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

    public override object Clone()
    {
        return new IronTower(upgrade1, upgrade2, upgrade3);
    }
}
