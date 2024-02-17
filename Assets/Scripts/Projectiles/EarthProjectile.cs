using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthProjectile : ProjectileCode
{
    public EarthProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3)
    {
        damage = 3;
        pierce = 10;
    }

    public override void Start(ProjectileController controller)
    {
        base.Start(controller);
        controller.transform.localScale *= 2;
    }
    public override void tick(ProjectileController controller)
    {
        //do projectile stuff
        if (target != null)
        {
            move = lvl * speed * (target.transform.position - controller.transform.position).normalized;
        }
        else
        {
            move.y = 0;
        }

        controller.transform.Translate(Time.deltaTime*move);
        Collider[] hit = Physics.OverlapSphere(controller.transform.position, .5f*MapCreator.scale, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < hit.Length; i++)
        {
            this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
        }
    }
}
