using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTower : TowerCode
{
    private int statIncreaseAmount = 0;

    public DarkTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        attackSpeed = 256;
        range = 1;
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override int getAttackSpeed()
    {
        switch (statIncreaseAmount)
        {
            case 0:
            case 1: return 256;
            case 2: 
            case 3: 
            case 4: return 192;
            case 5: 
            case 6:
            case 7: 
            case 8: return 128;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13: return 96;
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:  return 64;
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26: return 48;
        }
        return 32;
    }

    public override float getRange()
    {
        switch (statIncreaseAmount)
        {
            case 0:
            case 1: return base.getRange();
            case 2: 
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8: return base.getRange()+1;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 20:
            case 21:
            case 22:
            case 23: return base.getRange()+2;
        }

        return base.getRange() + 3;
    }

    public override void tick()
    {
        if (ticksLeft > 0)
        {
            ticksLeft -= lvl*Time.deltaTime*64f;
        }

        if (ticksLeft <= 0)
        {
            
            if (shoot())
            {
                int attackSpeed = getAttackSpeed();
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
                for (int i = 0; i < nextTo.Count; i++)
                {
                    if (nextTo[i].tower != null && !(nextTo[i].tower is DarkTower))
                    {
                        nextTo[i].tower.ticksLeft += attackSpeed * nextTo[i].tower.lvl * 0.25f;
                    }
                }
                ticksLeft = getAttackSpeed() + ticksLeft;
                rechargeTime = getAttackSpeed() - 1;
            }
        }
    }

    public override void roundStart()
    {
        base.roundStart();
        statIncreaseAmount = 0;
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
        for (int i = 0; i < nextTo.Count; i++)
        {
            if (nextTo[i].tower != null && !(nextTo[i].tower is DarkTower))
            {
                statIncreaseAmount += nextTo[i].tower.lvl;
            }
        }
    }

    public override ProjectileCode create()
    {
        return new DarkProjectile(upgrade1, upgrade2, upgrade3,statIncreaseAmount);
    }

    public override Color getColor()
    {
        return ColorManager.manager.darkTower;
    }

    public override object Clone()
    {
        return new DarkTower(upgrade1, upgrade2, upgrade3);
    }
}
