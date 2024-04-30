using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorTower : MultiTowerCode
{

    public ReactorTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override void tick()
    {
        List<TowerController> toBuff = PathfinderManager.manager.tiles;
        for (int i = 0; i < toBuff.Count; i++)
        {
            if (toBuff[i].tower != null)
            {
                toBuff[i].tower.ticksLeft -= Time.deltaTime * lvl * toBuff[i].tower.lvl * 10f;
                toBuff[i].tower.rechargeTime -= Time.deltaTime * lvl * toBuff[i].tower.lvl * 10f;
            }
        }
    }

    public override void roundStart()
    {
        base.roundStart();
        // create projectiles
        for (int i = 0; i < 4; i++)
        {
            GameObject go = Object.Instantiate(projectile);
            ProjectileController pc = go.GetComponent<ProjectileController>();
            pc.code = new ReactorProjectile(i,controller.transform.position,upgrade1,upgrade2,upgrade3);
            pc.code.lvl = lvl;
            go.transform.position = controller.towerVisual.shoot(rechargeTime);
            pc.material.color = getColor();
            pc.code.Start(pc);
        }
        
    }

    public override ProjectileCode create()
    {
        return new ReactorProjectile(0,new Vector3(),upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.atomicTower;
    }
}
