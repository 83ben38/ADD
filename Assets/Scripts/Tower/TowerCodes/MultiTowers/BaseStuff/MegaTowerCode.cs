using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MegaTowerCode : TowerCode
{
    public int towerEquivalent;
    protected MegaTowerCode(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
    }

    public override bool canMerge(TowerCode c)
    {
        return (c.lvl == lvl && c.GetType() == GetType() && lvl < 3) || (c.lvl==lvl+towerEquivalent-1 && c is ColorTower && lvl < 3 && !c.upgrade1);
    }
}
