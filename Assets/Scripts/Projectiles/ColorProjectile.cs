using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorProjectile : ProjectileCode
{
    private float time;

    public ColorProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        pierce = 1;
        damage = 1;
    }

    public override void tick(ProjectileController controller)
    {
        base.tick(controller);
        time += Time.deltaTime*6f;
        Color c;
        float d = time % 1;
        switch ((int)time%6)
        {
            case 0: c = new Color(1, d, 0, 1);
                break;
            case 1: c = new Color((1-d), 1, 0, 1);
                break;
            case 2: c = new Color(0, 1, d, 1);
                break;
            case 3: c = new Color(0, (1-d), 1, 1);
                break;
            case 4: c = new Color(d, 0, 1, 1);
                break;
            default: c = new Color(1, 0, (1-d), 1);
                break;
        }

        controller.material.color = c;
    }
}
