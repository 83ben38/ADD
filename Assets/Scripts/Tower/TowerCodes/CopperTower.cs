using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperTower : TowerCode
{
    private int charges = 0;
    private List<TowerController> nextTo = new List<TowerController>();

    public CopperTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        attackSpeed = 16;
        range = 1;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }
    

    public override void tick()
    {
        ticksLeft -= lvl*Time.deltaTime*64f;
        if (ticksLeft <= 0)
        {
            ticksLeft += getAttackSpeed();
            charges++;
        }
        for (int i = 0; i < nextTo.Count; i++)
        {
            if (nextTo[i].tower != null && nextTo[i].tower.ticksLeft < 0)
            {
                ticksLeft -= nextTo[i].tower.lvl*Time.deltaTime*64f;
                if (ticksLeft <= 0)
                {
                    ticksLeft += getAttackSpeed();
                    charges++;
                }
            }
        }

        shoot();
    }

    public override bool shoot()
    {
        if (charges == 0)
        {
            return false;
        }

        List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(self, getRange() * MapCreator.scale,LayerMask.GetMask("Enemy")));
        for (int i = 0; i < sphere.Count; i++)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl > 1 ? lvl : 2;
            pc.code.target = sphere[i].gameObject.GetComponent<FruitCode>();
            projectile.transform.position = controller.transform.position;
            pc.material.color = getColor();
            pc.code.Start(pc);
        }
        return true;
    }

    public override void roundStart()
    {
        base.roundStart();
        nextTo = new List<TowerController>(controller.nextTo);
        for (int k = 0; k < getRange()-1; k++)
        {
            int z = nextTo.Count;
            for (int i = 0; i < z; i++)
            {

                List<TowerController> nextToNextTo = nextTo[i].nextTo;

                for (int j = 0; j < nextToNextTo.Count; j++)
                {
                    if (! nextTo.Contains(nextToNextTo[j]) )
                    {
                            
                        nextTo.Add(nextToNextTo[j]);
                            
                    } 
                }
            }
        }
    }

    public override ProjectileCode create()
    {
        return new CopperProjectile(upgrade1,upgrade2,upgrade3,0);
    }

    public override Color getColor()
    {
        return ColorManager.manager.copperTower;
    }

    public override object Clone()
    {
        return new CopperTower(upgrade1, upgrade2, upgrade3);
    }
}
