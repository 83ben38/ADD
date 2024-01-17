using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTower : TowerCode
{
    private static Vector3[] directions = new[] {new Vector3(1,0,0),new Vector3(-1,0,0),new Vector3(0.5f,0,0.76f).normalized,new Vector3(0.5f,0,-0.76f).normalized,new Vector3(-0.5f,0,0.76f).normalized,new Vector3(-0.5f,0,-0.76f).normalized };
    public EarthTower()
    {
        
    }

    public override int getAttackSpeed()
    {
        return lvl  > 4  ? 128 : 96;
    }

    public override float getRange()
    {
        return -1;
    }

    public override bool shoot()
    {
        controller.towerVisual.shoot();

        foreach (var vector3 in directions)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl;
            pc.code.move = 2*vector3;
            projectile.transform.position = controller.transform.position+new Vector3(0,0.65f,0);
            pc.material.color = getColor();
            pc.code.Start();
        }
        return true;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool canMerge(TowerCode c)
    {
        return c.GetType() == typeof(EarthTower) && c.lvl == lvl;
    }

    public override ProjectileCode create()
    {
        return new EarthProjectile();
    }

    public override Color getColor()
    {
        return ColorManager.manager.earthTower;
    }
}
