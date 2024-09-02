using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using Math = System.Math;

public class SnowProjectile : ProjectileCode
{
    public float radius = 0.25f;
    public int layer = 1;
    public SnowProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        damage = 1;
        pierce = 3;
        if (upgrade1)
        {
            if (upgrade2)
            {
                radius = .5f;
            }

            radius = .5f + (lvl * 0.25f);
        }
    }
    public SnowProjectile(bool upgrade1, bool upgrade2, bool upgrade3, int  layer) : base(upgrade1, upgrade2, upgrade3)
    {
        damage = 1;
        pierce = 3;
        if (upgrade1)
        {
            if (upgrade2)
            {
                radius = .5f;
            }
            radius = .5f + (lvl * 0.25f);
        }

        this.layer = layer;
    }
    
    public override void tick(ProjectileController controller)
    {
        //do projectile stuff
        if (target != null)
        {
            move = speed * (target.transform.position - controller.transform.position).normalized;
            target = null;
        }
        else if (controller.transform.position.y <= 1)
        {
            move.y = 0;
        }

        if (upgrade1)
        {
            radius -= Time.deltaTime * 0.25f;
            damage = (int)(radius / 0.125f);
            controller.transform.localScale = Vector3.one * (radius * MapCreator.scale);
            controller.transform.Translate(Time.deltaTime*move);
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, radius*MapCreator.scale, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
            }
            if (radius < .25f)
            {
                if (upgrade2 && layer < lvl)
                {
                    Vector3 move1 =
                        new Vector3((float)(move.x * Math.Cos(Math.PI / 6) + move.z * Math.Sin(Math.PI / 6)), move.y,
                            (float)(move.z * Math.Cos(Math.PI / 6) + move.x * Math.Sin(Math.PI / 6)));
                    Vector3 move2 =
                        new Vector3((float)(move.x * Math.Cos(Math.PI / -6) + move.z * Math.Sin(Math.PI / -6)), move.y,
                            (float)(move.z * Math.Cos(Math.PI / -6) + move.x * Math.Sin(Math.PI / -6)));
                    GameObject projectile = Object.Instantiate(TowerCode.projectile);
                    ProjectileController pc = projectile.GetComponent<ProjectileController>();
                    pc.code = new SnowProjectile(upgrade1, upgrade2, upgrade3, layer + 1);
                    pc.code.lvl = lvl;
                    pc.code.move = move1;
                    projectile.transform.position = controller.transform.position;
                    pc.material.color = controller.material.color;
                    pc.code.Start(pc);
                    projectile = Object.Instantiate(TowerCode.projectile);
                    pc = projectile.GetComponent<ProjectileController>();
                    pc.code = new SnowProjectile(upgrade1, upgrade2, upgrade3, layer + 1);
                    pc.code.lvl = lvl;
                    pc.code.move = move2;
                    projectile.transform.position = controller.transform.position;
                    pc.material.color = controller.material.color;
                    pc.code.Start(pc);
                }
                Object.Destroy(controller.gameObject);
            }
        }
        else
        {
            radius += Time.deltaTime * 0.25f;
            damage = (int)(radius / 0.125f);
            controller.transform.localScale = Vector3.one * (radius * MapCreator.scale);
            controller.transform.Translate(Time.deltaTime*move);
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, radius*MapCreator.scale, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
            }
            if (radius > (upgrade2 ? .5f : .5f + (lvl * 0.25f)))
            {
                if (upgrade2 && layer < lvl)
                {
                    Vector3 move1 =
                        new Vector3((float)(move.x * Math.Cos(Math.PI / 6) + move.z * Math.Sin(Math.PI / 6)), move.y,
                            (float)(move.z * Math.Cos(Math.PI / 6) + move.x * Math.Sin(Math.PI / 6)));
                    Vector3 move2 =
                        new Vector3((float)(move.x * Math.Cos(Math.PI / -6) + move.z * Math.Sin(Math.PI / -6)), move.y,
                            (float)(move.z * Math.Cos(Math.PI / -6) + move.x * Math.Sin(Math.PI / -6)));
                    GameObject projectile = Object.Instantiate(TowerCode.projectile);
                    ProjectileController pc = projectile.GetComponent<ProjectileController>();
                    pc.code = new SnowProjectile(upgrade1, upgrade2, upgrade3, layer + 1);
                    pc.code.lvl = lvl;
                    pc.code.move = move1;
                    projectile.transform.position = controller.transform.position;
                    pc.material.color = controller.material.color;
                    pc.code.Start(pc);
                    projectile = Object.Instantiate(TowerCode.projectile);
                    pc = projectile.GetComponent<ProjectileController>();
                    pc.code = new SnowProjectile(upgrade1, upgrade2, upgrade3, layer + 1);
                    pc.code.lvl = lvl;
                    pc.code.move = move2;
                    projectile.transform.position = controller.transform.position;
                    pc.material.color = controller.material.color;
                    pc.code.Start(pc);
                }
                Object.Destroy(controller.gameObject);
            }
        }

        
    }
    public override int getDamage()
    {
        return base.getDamage()*2;
    }
    public virtual void hit(FruitCode fruit, ProjectileController controller)
    {
        if (pierced.Contains(fruit))
        {
            return;
        }
        pierced.Add(fruit);
        if (fruit.Equals(target))
        {
            target = null;
        }

        fruit.Damage(getDamage());
        fruit.Damage(0);
        fruit.Damage(0);
        pierceLeft--;
        if (pierceLeft < 1)
        {
            if (upgrade2 && layer < lvl)
            {
                Vector3 move1 =
                    new Vector3((float)(move.x * Math.Cos(Math.PI / 6) + move.z * Math.Sin(Math.PI / 6)), move.y,
                        (float)(move.z * Math.Cos(Math.PI / 6) + move.x * Math.Sin(Math.PI / 6)));
                Vector3 move2 =
                    new Vector3((float)(move.x * Math.Cos(Math.PI / -6) + move.z * Math.Sin(Math.PI / -6)), move.y,
                        (float)(move.z * Math.Cos(Math.PI / -6) + move.x * Math.Sin(Math.PI / -6)));
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = new SnowProjectile(upgrade1, upgrade2, upgrade3, layer + 1);
                pc.code.lvl = lvl;
                pc.code.move = move1;
                projectile.transform.position = controller.transform.position;
                pc.material.color = controller.material.color;
                pc.code.Start(pc);
                projectile = Object.Instantiate(TowerCode.projectile);
                pc = projectile.GetComponent<ProjectileController>();
                pc.code = new SnowProjectile(upgrade1, upgrade2, upgrade3, layer + 1);
                pc.code.lvl = lvl;
                pc.code.move = move2;
                projectile.transform.position = controller.transform.position;
                pc.material.color = controller.material.color;
                pc.code.Start(pc);
            }
            Object.Destroy(controller.gameObject);
        }
    }

    public override int getPierce()
    {
        return base.getPierce()*2;
    }
}
