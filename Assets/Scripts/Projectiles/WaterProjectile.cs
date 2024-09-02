using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaterProjectile : ProjectileCode
{
    public bool hitEnemy = false;
    private float explosionAmount = 1f;
    private Vector3 baseTransform;
    private bool secondary = false;
    private float ticksLeft = 1f;
    private static Vector3[] directions = new[] {new Vector3(1,0,0),new Vector3(-1,0,0),new Vector3(0.5f,0,0.76f).normalized,new Vector3(0.5f,0,-0.76f).normalized,new Vector3(-0.5f,0,0.76f).normalized,new Vector3(-0.5f,0,-0.76f).normalized };

    public WaterProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3)
    {
        damage = upgrade2 ? 1 : 2;
    }
    private WaterProjectile(bool upgrade1, bool upgrade2, bool upgrade3, Vector3 move) : base(upgrade1,upgrade2, upgrade3)
    {
        damage = 1;
        secondary = true;
        base.move = move;
    }
    public override void tick(ProjectileController controller)
    {
        if (damage == 2 && upgrade1)
        {
            Collider[] hitProjectiles = Physics.OverlapSphere(controller.transform.position, .25f * explosionAmount * MapCreator.scale);
            for (int i = 0; i < hitProjectiles.Length; i++)
            {
                ProjectileController pc = hitProjectiles[i].GetComponent<ProjectileController>();
                if (pc != null && pc.code is LightningProjectile)
                {
                    damage = 4;
                }
            }
        }

        if (!hitEnemy)
        {
            if (target != null)
            {
                move = lvl * speed * (target.transform.position - controller.transform.position).normalized;
            }
            else
            {
                move.y = 0;
            }
            controller.transform.Translate(Time.deltaTime * move);
            if (secondary)
            {
                ticksLeft -= Time.deltaTime * lvl;
                if (ticksLeft <= 0)
                {
                    baseTransform = controller.transform.localScale;
                    hitEnemy = true;
                    if (lvl > 3)
                    {
                        foreach (var vector3 in directions)
                        {
                            GameObject projectile = Object.Instantiate(TowerCode.projectile);
                            ProjectileController pc = projectile.GetComponent<ProjectileController>();
                            pc.code = new WaterProjectile(upgrade1,upgrade2,upgrade3,vector3*(MapCreator.scale*(lvl-3)*0.5f));
                            pc.code.lvl = lvl-3;
                            projectile.transform.position =
                                controller.transform.position;
                            pc.material.color = controller.material.color;
                            pc.code.Start(pc);
                        }
                    }
                }
            }
            else
            {
                Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f * MapCreator.scale,
                    LayerMask.GetMask("Enemy"));
                for (int i = 0; i < hit.Length; i++)
                {
                    if (i == 0)
                    {
                        SoundEffectsManager.manager.playSound("splash");
                    }

                    if (!hitEnemy && upgrade2)
                    {
                            foreach (var vector3 in directions)
                            {
                                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                                pc.code = new WaterProjectile(upgrade1,upgrade2,upgrade3,vector3*(MapCreator.scale* lvl*0.5f));
                                pc.code.lvl = lvl;
                        
                                projectile.transform.position =
                                    controller.transform.position;
                                pc.material.color = controller.material.color;
                                pc.code.Start(pc);
                            }
                    }
                    this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
                }

                if (hit.Length > 0)
                {
                    baseTransform = controller.transform.localScale;
                }
            }
        }
        else
        {
            explosionAmount += Time.deltaTime;
            controller.transform.localScale = baseTransform * explosionAmount;
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f*explosionAmount*MapCreator.scale, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
            }

            if (explosionAmount >= ((lvl*0.5f /(secondary ? 2 : 1)) + 1))
            {
                

                Object.Destroy(controller.gameObject);
            }
        }
    }

    public override void hit(FruitCode fruit, ProjectileController controller)
    {
        if (pierced.Contains(fruit))
        {
            return;
        }
        
        hitEnemy  = true;
        pierced.Add(fruit);
        fruit.Damage(getDamage());
    }
}
