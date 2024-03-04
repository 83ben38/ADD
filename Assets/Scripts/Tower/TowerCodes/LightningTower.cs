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
        List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(self, getRange() * MapCreator.scale,LayerMask.GetMask("Enemy")));
        if (sphere.Count > 0)
        {
            for (int i = 0; (i < lvl && upgrade1) || i < 1; i++)
            {
                if (sphere.Count == i)
                {
                    break;
                }
                FruitCode fc = sphere[i].gameObject.GetComponent<FruitCode>();
                if (fc.hidden)
                {
                    sphere.RemoveAt(0);
                    i--;
                    continue;
                }
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = create();
                pc.code.lvl = lvl > 1 ? lvl : 2;
                pc.code.target = fc;
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
