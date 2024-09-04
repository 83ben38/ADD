using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertiaProjectile : ProjectileCode
{
    public static FruitCode lastHit;
    public static int damageCombo = -1;
    public static List<ProjectileCode> affected =  new List<ProjectileCode>();
    public InertiaProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        pierce = 1;
        damage = 3;
    }

    public override void tick(ProjectileController controller)
    {
        base.tick(controller);
        if (upgrade3)
        {
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f*MapCreator.scale, LayerMask.GetMask("Projectile"));
            foreach (var t in hit)
            {
                ProjectileCode c = t.GetComponent<ProjectileController>().code;
                if (!affected.Contains(c) && c is not InertiaProjectile)
                {
                    c.damage++;
                    c.move *= 2f;
                    Object.Destroy(controller.gameObject);
                }
            }
        }
    }

    public override void hit(FruitCode fruit, ProjectileController controller)
    {
        if (upgrade2)
        {
            if (fruit == lastHit)
            {
                damageCombo++;
            }
            else
            {
                damageCombo = -1;
                lastHit = fruit;
            }

            fruit.Damage((damage+damageCombo)*lvl);
        }
        else
        {
            fruit.Damage(getDamage());
        }

        Object.Destroy(controller.gameObject);
    }
}
