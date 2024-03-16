using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSmallProjectile : ProjectileCode
{
    
    public LifeSmallProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        damage = 1;
        pierce = 1;
    }

    public override int getDamage()
    {
        switch (lvl)
        {
            case 5: return 3;
            case 6: return 4;
            case 7: return 5;
        }

        return 2;
    }

    public override void tick(ProjectileController controller)
    {
        if (target != null)
        {
            move = lvl * speed * (target.transform.position - controller.transform.position).normalized;
        }
        else
        {
            move.y = 0;
        }

        controller.transform.Translate(Time.deltaTime*move);
        Collider[] hit = Physics.OverlapSphere(controller.transform.position, .125f*MapCreator.scale, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < hit.Length; i++)
        {
            this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
        }
    }
}
