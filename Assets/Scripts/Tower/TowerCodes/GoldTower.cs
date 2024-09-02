using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.Timeline;

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
        base.roundStart();
        turns++;
        if (turns == ((upgrade1 ? 7 : 3) + (upgrade2 ? 1 : 0)))
        {
            int[] towerCodes = SelectionData.data.towerCodes;
            TowerCode t = TowerCodeFactory.getTowerCode(towerCodes[Random.Range(0, towerCodes.Length)]);
            t.lvl = lvl + (upgrade1 ? 2 : 1) + (upgrade2 ? -1 : 0);
            if (t.lvl > 7)
            {
                t.lvl=7;
            }

            if (upgrade2)
            {
                List<TowerController> nextTo = controller.nextTo;
                foreach (TowerController tc in nextTo)
                {
                    if (tc.tower == null)
                    {
                        tc.block = true;
                        if (PathfinderManager.manager.pathFind())
                        {
                            tc.StartCoroutine(InGameState.changeTowerStatic(tc ,true));
                            tc.setBaseColor(ColorManager.manager.tower,ColorManager.manager.towerHighlighted);
                            tc.tower = t;
                            tc.tower.placedDown(controller);
                            tc.state = t;
                            tc.towerVisual.updateTower();
                            turns = 0;
                            return;
                        }
                        tc.block = false;
                    }
                }
            }
            else
            {
                controller.tower = t;
                controller.state = t;
                controller.towerVisual.updateTower();
            }
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
