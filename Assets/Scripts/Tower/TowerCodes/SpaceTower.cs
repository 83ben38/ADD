using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceTower : TowerCode
{
    public SpaceTower()
    {
        attackSpeed = 192;
        
    }

    public override float getRange()
    {
        return -1.5f;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }
    public override bool shoot()
    {
        Collider[] sphere = Physics.OverlapSphere(self, 25f,LayerMask.GetMask("Enemy"));
        if (sphere.Length > 0)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl;
            pc.code.target = sphere[0].GameObject().GetComponent<FruitCode>();
            projectile.transform.position = controller.towerVisual.shoot();
            pc.material.color = getColor();
            pc.code.Start(pc);
            return true;
        }
        return false;
    }
    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == GetType() && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return new SpaceProjectile();
    }

    public override Color getColor()
    {
        return ColorManager.manager.spaceTower;
    }
}
