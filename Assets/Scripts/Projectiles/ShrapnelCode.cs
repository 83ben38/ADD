using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelCode : ProjectileCode
{
    public float ticksLeft;
    public ShrapnelCode(bool upgrade1, bool upgrade2, bool upgrade3, Vector3 move) : base(upgrade1, upgrade2, upgrade3)
    {
        base.move = move;
        ticksLeft = 2;
        damage = 1;
    }

    public override int getPierce()
    {
        return 2;
    }

    

    public override void tick(ProjectileController controller)
    {
        base.tick(controller);
        ticksLeft -= Time.deltaTime;
        if (ticksLeft <= 0)
        {
           Object.Destroy(controller.gameObject);
        }
    }
}
