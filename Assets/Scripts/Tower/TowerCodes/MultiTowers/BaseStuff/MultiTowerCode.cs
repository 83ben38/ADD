using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiTowerCode : TowerCode
{
    public Dictionary<TowerController, TowerCode> oldTowers;
    protected MultiTowerCode(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        oldTowers = new Dictionary<TowerController, TowerCode>();
    }

    public override bool canMerge(TowerCode c)
    {
        return false;
    }
    public override void pickedUp()
    {
        foreach (var pair in oldTowers)
        {
            pair.Key.tower = pair.Value;
            pair.Key.towerVisual.updateTower();
            pair.Key.setBaseColor(ColorManager.manager.tower,ColorManager.manager.towerHighlighted);
        }
    }
    public override object Clone()
    {
        return null;
    }
}
