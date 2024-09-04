using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class WaterTower : TowerCode
{
    public int charges = 0;
    public WaterTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 2;
        attackSpeed = (int)(attackSpeed * (upgrade1 ? 2.5 : 2));
    }

    public override void MouseClick(TowerController controller)
    {
        charges++;
        ticksLeft += getAttackSpeed();
    }
    public override bool shoot()
    {
        List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(self, getRange() * MapCreator.scale,LayerMask.GetMask("Enemy")));
        while (sphere.Count > 0 && sphere[0].gameObject.GetComponent<FruitCode>().hidden)
        {
            sphere.RemoveAt(0);
        }
        if (sphere.Count > 0)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = (lvl > 1 ? lvl : 2) + (upgrade3 ? (int)Math.Log(charges+1,2) : 0);
            charges = 0;
            pc.code.target = sphere[0].gameObject.GetComponent<FruitCode>();
            projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
            pc.material.color = getColor();
            pc.code.Start(pc);
            return true;
        }
        return false;
    }
    

    public override ProjectileCode create()
    {
        return new WaterProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.waterTower;
    }

    public override object Clone()
    {
        return new WaterTower(upgrade1, upgrade2, upgrade3);
    }
}
