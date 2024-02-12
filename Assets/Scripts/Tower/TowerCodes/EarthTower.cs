using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTower : TowerCode
{
    private static Vector3[] directions = new[] {new Vector3(1,0,0),new Vector3(-1,0,0),new Vector3(0.5f,0,0.76f).normalized,new Vector3(0.5f,0,-0.76f).normalized,new Vector3(-0.5f,0,0.76f).normalized,new Vector3(-0.5f,0,-0.76f).normalized };
    public EarthTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        
    }

    public override int getAttackSpeed()
    {
        return lvl  > 4  ? 128 : 96;
    }

    public override float getRange()
    {
        return -1.5f;
    }

    public override bool shoot()
    {
        controller.towerVisual.shoot(rechargeTime);

        foreach (var vector3 in directions)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl;
            pc.code.move = MapCreator.scale*2*vector3;
            projectile.transform.position = controller.transform.position+new Vector3(0,0.65f*MapCreator.scale,0);
            pc.material.color = getColor();
            pc.code.Start(pc);
        }
        return true;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    

    public override ProjectileCode create()
    {
        return new EarthProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.earthTower;
    }
}
