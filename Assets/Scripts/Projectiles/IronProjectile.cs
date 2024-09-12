using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class IronProjectile : ProjectileCode
{
    public TowerController targetPath;
    private Vector3 targetPosition;
    public ProjectileController stickTo;
    public float explode = 0;
    
    public override void Start(ProjectileController controller)
    {
        damage = 1;
        if (upgrade3)
        {
            damage++;
        }

        base.Start(controller);
        targetPosition = targetPath.transform.position;
        targetPosition.y += 0.5f * MapCreator.scale;
        Vector2 circle = Random.insideUnitCircle * 0.4f * MapCreator.scale;
        targetPosition.x += circle.x;
        targetPosition.z += circle.y;
        speed *= 1.5f;
    }

    public override int getPierce()
    {
        return lvl*2;
    }
    public virtual void hit(FruitCode fruit, ProjectileController controller)
    {
        
        SoundEffectsManager.manager.playSound("iron_hit");
        if (fruit.Equals(target))
        {
            target = null;
        }

        int z = 1;
        if (fruit is CoconutCode code)
        {
            z = code.reductionMod+1;
        }
        int damage = Math.Min((fruit.hp + fruit.vulnerability)*z, pierceLeft*base.damage);
        damage /= z;
        damage += z-1;
        fruit.Damage(damage);
        pierceLeft -= (damage / base.damage);
        if (pierceLeft < 1)
        {
            Object.Destroy(controller.gameObject);
        }
    }
    public override void tick(ProjectileController controller)
    {
        //do projectile stuff
        if (explode > 0)
        {
            explode += Time.deltaTime;
            Color c = controller.material.color;
            c.a = 0.5f;
            controller.material.color = c;
            controller.transform.localScale = new Vector3(explode, explode, explode);
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, explode*MapCreator.scale, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                FruitCode fc = hit[i].GetComponent<FruitCode>();
                if (!pierced.Contains(fc))
                {
                    fc.Damage(damage * getPierce());
                }
            }

            if (explode > 1)
            {
                Object.Destroy(controller.gameObject);
            }
        }
        else if (target != null)
        {
            move = lvl * MapCreator.scale * speed * (target.transform.position - controller.transform.position).normalized;
            controller.transform.Translate(Time.deltaTime * move);
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f*MapCreator.scale, LayerMask.GetMask("Enemy"));
            if (hit.Length > 0)
            {
                this.hit(hit[0].gameObject.GetComponent<FruitCode>(),controller);
            }
        }
        else { 
            if (upgrade2 && stickTo != null)
            {
                if ((stickTo.transform.position - controller.transform.position ).magnitude < 0.375f)
                {
                    move = stickTo.code.move;
                }
                else
                {
                    move = lvl * MapCreator.scale * speed * (stickTo.transform.position - controller.transform.position).normalized;
                }

                controller.transform.Translate(Time.deltaTime * move);
            }
            else if (targetPosition != Vector3.zero)
            {
                move = lvl * MapCreator.scale * speed * (targetPosition - controller.transform.position).normalized;
                controller.transform.Translate(Time.deltaTime * move);
                if ((targetPosition - controller.transform.position).magnitude < 0.05f*lvl*MapCreator.scale)
                {
                    targetPosition = Vector3.zero;
                    
                }
            }

            if (!upgrade3)
            {
                Collider[] hit = Physics.OverlapSphere(controller.transform.position, MapCreator.scale,
                    LayerMask.GetMask("Enemy"));
                if (hit.Length > 0)
                {
                    target = hit[0].gameObject.GetComponent<FruitCode>();
                }
            }

            Collider[] hit2 = Physics.OverlapSphere(controller.transform.position, .05f*MapCreator.scale, LayerMask.GetMask("Projectile"));
            for (int i = 0; i < hit2.Length; i++)
            {
                ProjectileController pc = hit2[i].gameObject.GetComponent<ProjectileController>();
                if (pc != null)
                {
                    if (pc.code is IronProjectile && pc != controller)
                    {
                        pierceLeft += pc.code.pierceLeft;
                        Object.DestroyImmediate(pc.gameObject);
                    }
                }
            }

            if (upgrade2 && stickTo == null)
            {
                hit2 = Physics.OverlapSphere(controller.transform.position, MapCreator.scale,
                    LayerMask.GetMask("Projectile"));
                for (int i = 0; i < hit2.Length; i++)
                {
                    ProjectileController pc = hit2[i].gameObject.GetComponent<ProjectileController>();
                    if (pc != null)
                    {
                        if (pc.code is EarthProjectile)
                        {
                            stickTo = pc;
                            damage += 2;
                        }
                    }
                }
            }
        }
    }

    public IronProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
    }
}
