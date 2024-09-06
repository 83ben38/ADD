using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperTower : TowerCode
{
    
    private int charges = 0;
    private static int sharedCharges = 0;
    private bool contributed = false;
    private static int sharedMaxCharges = 0;
    private bool chargeMode = true;
    private List<TowerController> nextTo = new List<TowerController>();
    private List<CopperProjectile> projectiles = new List<CopperProjectile>();
    private GameObject chargeObject;
    //upgrade 1 electroconductivity
    //upgrade 2 the grid
    public CopperTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        attackSpeed = 16;
        if (upgrade1)
        {
            attackSpeed += 4;
        }

        range = 1;
    }

    public override void MouseClick(TowerController controller)
    {
        chargeMode = !chargeMode;
    }
    

    public override void tick()
    {

        if (!upgrade3 || chargeMode)
        {
            ticksLeft -= lvl * Time.deltaTime * 32f * (upgrade3 ? 2 : 1);
            if (ticksLeft <= 0)
            {
                ticksLeft += getAttackSpeed();
                charges++;
            }

            for (int i = 0; i < nextTo.Count; i++)
            {
                if (nextTo[i].tower != null && nextTo[i].tower.ticksLeft < 0)
                {
                    float chargeFactor = upgrade2 ? lvl / 6f : 1f;
                    if (upgrade1 && nextTo[i].tower is LightningTower)
                    {
                        ticksLeft -= nextTo[i].tower.lvl * Time.deltaTime * 64f * chargeFactor;
                    }

                    ticksLeft -= nextTo[i].tower.lvl * Time.deltaTime * 64f * chargeFactor;
                    if (ticksLeft <= 0)
                    {
                        ticksLeft += getAttackSpeed();
                        charges++;
                    }
                }
            }

            if (upgrade2)
            {
                if (!contributed)
                {
                    sharedMaxCharges += lvl * 15 * (upgrade3 ? 2 : 1);
                    contributed = true;
                }

                sharedCharges += charges;
                charges = 0;
                if (sharedCharges > sharedMaxCharges)
                {
                    chargeMode = false;
                    sharedCharges = sharedMaxCharges;
                }
            }
            else if (charges > lvl * 20 * (upgrade3 ? 2 : 1))
            {
                chargeMode = false;
                charges = lvl * 20 * (upgrade3 ? 2 : 1);
            }
        }

        if (!upgrade3 || !chargeMode)
        {
            chargeMode = !shoot();
        }

        if (upgrade2)
        {
            chargeObject.transform.localScale =
                Vector3.one * ((.25f+(sharedCharges / (sharedMaxCharges * 1.5f))) * MapCreator.scale);
        }
        else
        {
            chargeObject.transform.localScale =
                Vector3.one * ((.25f+(charges / (lvl * 30f * (upgrade3 ? 2 : 1)))) * MapCreator.scale);
        }
    }

    public override bool shoot()
    {
        if ((upgrade2 ? sharedCharges : charges) == 0)
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
            v.y += 1.6f*MapCreator.scale;
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

            if ((upgrade2 ? sharedCharges : charges) > 0)
            {
                projectiles[i].doDamage();
                if (upgrade2)
                {
                    sharedCharges--;
                }
                else
                {
                    charges--;
                }
            }
        }

        if ((upgrade2 ? sharedCharges : charges) == 0)
        {
            while (projectiles.Count > 0)
            {
                projectiles[0].destroy = true;
                projectiles.RemoveAt(0);
            }
        }

        return true;
    }

    public override void roundStart()
    {
        base.roundStart();
        charges = 0;
        chargeObject = Object.Instantiate(projectile);
        chargeObject.transform.localScale = new Vector3(.25f, .25f, .25f)*MapCreator.scale;
        chargeObject.GetComponent<ProjectileController>().material.color = getColor();
        Vector3 v = controller.transform.position;
        v.y += 1.6f*MapCreator.scale;
        chargeObject.transform.position = v;
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

        if (upgrade2)
        {
            sharedCharges = 0;
            sharedMaxCharges = 0;
            contributed = false;
        }
    }

    public override ProjectileCode create()
    {
        return new CopperProjectile(upgrade1,upgrade2,upgrade3,3);
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
