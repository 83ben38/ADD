using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowProjectile : ProjectileCode
{
    public float radius = 0.25f;   
    public SnowProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        damage = 1;
        pierce = 3;
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

        radius += Time.deltaTime * 0.25f;
        damage = (int)(radius / 0.125f);
        controller.transform.localScale = Vector3.one * (radius * MapCreator.scale);
        controller.transform.Translate(Time.deltaTime*move);
        Collider[] hit = Physics.OverlapSphere(controller.transform.position, radius*MapCreator.scale, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < hit.Length; i++)
        {
            this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
        }
        if (radius > .5f + (lvl * 0.25f))
        {
            Object.Destroy(controller.gameObject);
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
            Object.Destroy(controller.gameObject);
        }
    }

    public override int getPierce()
    {
        return base.getPierce()*2;
    }
}
