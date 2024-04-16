using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

public abstract class TowerCode : TowerState, ICloneable
{
    
    public static GameObject projectile;
    [Header("Stats")]
    public int attackSpeed = 64;
    public float range = 1;
    public int lvl;
    [Header("Vars")] 
    public float ticksLeft;

    public float rechargeTime;
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

    public virtual void placedDown(TowerController tc)
    {
        controller = tc;
    }
    public virtual void pickedUp()
    {
        controller = null;
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

        if (ticksLeft <= 0)
        {
            
            if (shoot())
            {
                ticksLeft = getAttackSpeed() + ticksLeft;
                rechargeTime = getAttackSpeed() - 1;
            }
        }
    }

    public virtual bool shoot()
    {
        List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(self, getRange() * MapCreator.scale,LayerMask.GetMask("Enemy")));
        while (sphere.Count > 0 && sphere[0].gameObject.GetComponent<FruitCode>().hidden)
        {
            sphere.RemoveAt(0);
        }
        if (sphere.Count > 0)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl > 1 ? lvl : 2;
            pc.code.target = sphere[0].gameObject.GetComponent<FruitCode>();
            projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
            pc.material.color = getColor();
            pc.code.Start(pc);
            return true;
        }
        return false;
    }

    public virtual bool canMerge(TowerCode c)
    {
        if (c is MegaTowerCode)
        {
            return false;
        }

        return (c.lvl == lvl && c.GetType() == GetType() && lvl < 6) || (c.lvl==lvl && c is ColorTower && lvl < 7 && !c.upgrade1);
    }

    public virtual TowerCode merge(TowerCode c)
    {
        lvl++;
        return this;
    }

    public virtual void roundStart()
    {
        List<StructureScriptableObject> options =
            StructureManager.manager.getPotentialStructures(TowerCodeFactory.getTowerCodeID(this));
        foreach (StructureScriptableObject sso in options)
        {
            if (lvl != sso.centerLevel)
            {
                continue;
            }

            int[] d = sso.getConfig();
            int[] l = sso.getLevels();
            List<TowerController> remove = new List<TowerController> { controller };
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] == -1)
                {
                    continue;
                }

                int z = i;
                List<TowerController> nextTo = controller.nextTo;
                while (z > nextTo.Count)
                {
                    nextTo = nextTo[z % nextTo.Count].nextTo;
                    z /= nextTo.Count;
                }

                if (nextTo[z].tower is null || nextTo[z].tower.lvl != l[i] || TowerCodeFactory.getTowerCodeID(nextTo[z].tower) != d[i])
                {
                    goto fail;
                }
                remove.Add(nextTo[z]);
            }
            controller.tower = sso.makeTower();
            controller.tower.controller = controller;
            foreach (TowerController tc in remove)
            {
                tc.tower = controller.tower;
                tc.towerVisual.updateTower();
                tc.setBaseColor(ColorManager.manager.structure,ColorManager.manager.structureHighlighted);
                tc.state = tc.tower;
            }
            return;
            fail : ;
        }
    }


    public abstract ProjectileCode create();
    public abstract Color getColor();
    public abstract object Clone();

}
