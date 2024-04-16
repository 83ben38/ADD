using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MegaTowerCode : TowerCode
{
    private Dictionary<TowerController, TowerCode> oldTowers;
    protected MegaTowerCode(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
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
