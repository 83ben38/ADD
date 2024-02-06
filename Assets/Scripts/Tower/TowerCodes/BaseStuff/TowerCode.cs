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
    public float ticksLeft;

    public bool upgrade1, upgrade2, upgrade3;
    public Vector3 self;
    public TowerController controller;
    public override void Run(TowerController c)
    {
        self = c.transform.position + new Vector3(0,0.5f,0);
        controller = c;
        tick();
    }

    public TowerCode(bool upgrade1, bool upgrade2, bool upgrade3)
    {
        this.upgrade1 = upgrade1;
        this.upgrade2 = upgrade2;
        this.upgrade3 = upgrade3;
        ticksLeft = getAttackSpeed();
    }

    public virtual int getAttackSpeed()
    {
        return attackSpeed;
    }

    public virtual float getRange()
    {
        return range + (lvl > 4 ? 1.0f : 0.0f);
    }

    

    public virtual void tick()
    {
        if (ticksLeft > 0)
        {
            ticksLeft -= lvl*Time.deltaTime*64f;
        }

        if (ticksLeft < 1)
        {
            
            if (shoot())
            {
                ticksLeft = getAttackSpeed() + ticksLeft;
            }
        }
    }

    public virtual bool shoot()
    {
        Collider[] sphere = Physics.OverlapSphere(self, getRange() * MapCreator.scale,LayerMask.GetMask("Enemy"));
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

    public abstract bool canMerge(TowerCode c);

    public abstract ProjectileCode create();
    public abstract Color getColor();
}
