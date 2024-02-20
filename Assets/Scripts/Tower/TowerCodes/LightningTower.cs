using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : TowerCode
{

    public LightningTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 3;
        attackSpeed = 128;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool shoot()
    {
        Collider[] sphere = Physics.OverlapSphere(self, getRange() * MapCreator.scale,LayerMask.GetMask("Enemy"));
        if (sphere.Length > 0)
        {
            for (int i = 0; i < lvl || !upgrade1; i++)
            {
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = create();
                pc.code.lvl = lvl;
                pc.code.target = sphere[i].gameObject.GetComponent<FruitCode>();
                projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
                pc.material.color = getColor();
                pc.code.Start(pc);
            }
            return true;
        }
        return false;
    }

    public override ProjectileCode create()
    {
        return new LightningProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.lightningTower;
    }
}
