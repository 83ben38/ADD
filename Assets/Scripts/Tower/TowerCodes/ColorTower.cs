using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTower : TowerCode
{
    private float time;
    public ColorTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        attackSpeed = 192;
        range = 2;
        time = 4.5f;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override bool canMerge(TowerCode c)
    {
        return c.lvl == lvl;
    }

    public override TowerCode merge(TowerCode c)
    {
        c.lvl++;
        return c;
    }

    public override ProjectileCode create()
    {
        return new ColorProjectile(upgrade1,upgrade2,upgrade3,time);
    }

    public override Color getColor()
    {
        return new Color(0.5f, 0, 1, 1);
    }

    public override void tick()
    {
        base.tick();
        time += Time.deltaTime;
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
        controller.towerVisual.setColor(c);
    }
}
