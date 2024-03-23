using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTower : TowerCode
{
    

    public LifeTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        attackSpeed = upgrade1 ? 512 : 256;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool shoot()
    {
        GameObject projectile = Object.Instantiate(TowerCode.projectile);
        ProjectileController pc = projectile.GetComponent<ProjectileController>();
        pc.code = create();
        pc.code.lvl = lvl > 1 ? lvl : 2;
        Vector2 rand = Random.insideUnitCircle*(MapCreator.scale*getRange());
        Vector3 pos = controller.transform.position;
        ((LifeProjectile)pc.code).target = new Vector3(pos.x+rand.x,pos.y+(1.8f*MapCreator.scale),pos.z+rand.y);
        ((LifeProjectile)pc.code).start = new Vector3(pos.x,pos.y+(1.8f*MapCreator.scale),pos.z);
        projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
        pc.material.color = getColor();
        pc.code.Start(pc);
        return true;
    }

    public override ProjectileCode create()
    {
        return new LifeProjectile(upgrade1, upgrade2, upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.lifeTower;
    }

    public override object Clone()
    {
        return new LifeTower(upgrade1, upgrade2, upgrade3);
    }
}
