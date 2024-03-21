using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperTower : TowerCode
{
    private int charges = 0;
    private List<TowerController> nextTo = new List<TowerController>();
    private List<CopperProjectile> projectiles = new List<CopperProjectile>();

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

        if (charges > lvl * 20)
        {
            charges = lvl * 20;
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
            FruitCode fc = sphere[i].gameObject.GetComponent<FruitCode>();
            for (int j = 0; j < projectiles.Count; j++)
            {
                if (projectiles[j].target == fc)
                {
                    goto end;
                }
            }
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = new CopperProjectile(upgrade1,upgrade2,upgrade3,lvl > 1 ? lvl : 2);
            pc.code.target = fc;
            Vector3 v = controller.transform.position;
            v.y += 1.45f;
            projectile.transform.position = v;
            pc.material.color = getColor();
            pc.code.Start(pc);
            projectiles.Add(pc.code as CopperProjectile);
            end: ;
        }

        for (int i = 0; i < projectiles.Count; i++)
        {
            if (projectiles[i].target == null)
            {
                projectiles[i].destroy = true;
            }

            if (projectiles[i].destroy)
            {
                projectiles.RemoveAt(i);
                i--;
                continue;
            }

            if (charges > 0)
            {
                projectiles[i].doDamage();
                charges--;
            }
        }

        if (charges == 0)
        {
            while (projectiles.Count > 0)
            {
                projectiles[0].destroy = true;
            }
        }

        return true;
    }

    public override void roundStart()
    {
        base.roundStart();
        projectiles = new List<CopperProjectile>();
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
