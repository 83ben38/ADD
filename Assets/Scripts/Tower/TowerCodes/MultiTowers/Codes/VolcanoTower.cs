using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoTower : MultiTowerCode
{
    

    public VolcanoTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 3;
        attackSpeed = 256;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }
    

    public override ProjectileCode create()
    {
        return new VolcanoProjectile(upgrade1, upgrade2, upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.volcanoTower;
    }
    public override bool shoot()
    {
        List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(self, getRange() * MapCreator.scale,LayerMask.GetMask("Enemy")));
        if (sphere.Count > 0)
        {
            for (int i = 0; i < lvl * 4; i++)
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
                pc.code.lvl = lvl;
                pc.code.target = fc;
                projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
                pc.material.color = getColor();
                pc.code.Start(pc);
            }
            return true;
        }
        return false;
    }
}
