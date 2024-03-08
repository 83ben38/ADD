using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTower : TowerCode
{
    private new GameObject projectile;
    private int turns=0;

    public GoldTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
    }

    public override float getRange()
    {
        return -1.5f;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override void tick()
    {
        
    }

    public override void roundStart()
    {
        turns++;
        if (turns == 3)
        {
            int[] towerCodes = SelectionData.data.towerCodes;
            TowerCode t = TowerCodeFactory.getTowerCode(towerCodes[Random.Range(0, towerCodes.Length)]);
            t.lvl = lvl + 1;
            if (t.lvl > 7)
            {
                t.lvl=7;
            }
            controller.tower = t;
            controller.state = t;
            controller.towerVisual.updateTower();
        }
    }

    public override ProjectileCode create()
    {
        return new GoldProjectile(upgrade1, upgrade2, upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.goldTower;
    }

    public override object Clone()
    {
        GoldTower gt = new GoldTower(upgrade1, upgrade2, upgrade3);
        gt.turns = turns;
        return gt;
    }
}
