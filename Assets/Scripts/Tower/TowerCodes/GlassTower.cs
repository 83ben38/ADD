using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTower : TowerCode
{
    private List<TowerCode> buffed = new List<TowerCode>();
    private float rangeBuff;
    public GlassTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3) {
        range = 1;
        attackSpeed = 0;
    }

    public override float getRange()
    {
        return range + (lvl/2);
    }

    public override void MouseClick(TowerController controller)
    { 
        
    }

    public override int getAttackSpeed()
    {
        return 0;
    }

    public override void roundStart()
    {
        base.roundStart();
        while (buffed.Count != 0)
        {
            buffed[0].range -= rangeBuff;
            buffed.RemoveAt(0);
        }

        List<TowerController> nextTo = new List<TowerController>(controller.nextTo);
        for (int k = 0; k < getRange()-1; k++)
        {
            int z = nextTo.Count;
            for (int i = 0; i < z; i++)
            {

                List<TowerController> nextToNextTo = nextTo[i].nextTo;

                for (int j = 0; j < nextToNextTo.Count; j++)
                {
                    if (! nextTo.Contains(nextToNextTo[j]) )
                    {
                            
                        nextTo.Add(nextToNextTo[j]);
                            
                    } 
                }
            }
        }

        rangeBuff = ((lvl + 1) / 2) * 0.3f;
        for (int i = 0; i < nextTo.Count; i++)
        {
            if (nextTo[i].tower != null && !(nextTo[i].tower is GlassTower))
            {
                nextTo[i].tower.range += rangeBuff;
                buffed.Add(nextTo[i].tower);
            }
        }
    }


    public override ProjectileCode create()
    {
        return null;
    }

    public override Color getColor()
    {
       return ColorManager.manager.glassTower;
    }

    public override object Clone()
    {
        return new GlassTower(upgrade1, upgrade2, upgrade3);
    }

    public override void tick()
    {
        



    }
}
