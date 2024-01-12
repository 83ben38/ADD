using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TowerCode : TowerState
{
    public static GameObject projectile;
    [Header("Stats")]
    public int attackSpeed = 64;
    public float range = 1;
    public int lvl;
    [Header("Vars")] 
    public int ticksLeft;

    public Transform self;
    public TowerController controller;
    public override void Run(TowerController c)
    {
        self = c.transform;
        controller = c;
        tick();
    }

    public TowerCode()
    {
        ticksLeft = attackSpeed;
    }


    public int getRange()
    {
        return range + lvl > 4 ? 1 : 0;
    }

    

    public void tick()
    {
        if (ticksLeft > 0)
        {
            ticksLeft -= lvl;
        }

        if (ticksLeft < 1)
        {
            
            if (shoot())
            {
                ticksLeft = attackSpeed + ticksLeft;
            }
        }
    }

    public bool shoot()
    {
        Collider[] sphere = Physics.OverlapSphere(self.position, range+1,LayerMask.GetMask("Enemy"));
        if (sphere.Length > 0)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl;
            pc.code.target = sphere[0].GameObject().GetComponent<FruitCode>();
            projectile.transform.position = controller.towerVisual.shoot();
            pc.material.color = getColor();
            return true;
        }
        return false;
    }

    public abstract bool canMerge(TowerCode c);

    public abstract ProjectileCode create();
    public abstract Color getColor();
}
