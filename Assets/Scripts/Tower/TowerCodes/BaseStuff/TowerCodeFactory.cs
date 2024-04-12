using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCodeFactory : MonoBehaviour
{
    public static TowerCode getTowerCode(int i)
    {
        bool b1 = SaveData.save.isUpgradeEnabled(i, 0);
        bool b2 = SaveData.save.isUpgradeEnabled(i, 1);
        bool b3 = SaveData.save.isUpgradeEnabled(i, 2);
        switch (i)
        {
            case 0 : return new FireTower(b1,b2,b3);
            case 1 : return new WaterTower(b1,b2,b3);
            case 2 : return new IceTower(b1,b2,b3);
            case 3 : return new LightningTower(b1,b2,b3);
            case 4 : return new EarthTower(b1,b2,b3);
            case 5 : return new IronTower(b1,b2,b3);
            case 6 : return new AtomicTower(b1,b2,b3);
            case 7 : return new SpaceTower(b1,b2,b3);
            case 8 : return new WindTower(b1,b2,b3);
            case 9 : return new ColorTower(b1, b2, b3);
            case 10: return new PoisonTower(b1, b2, b3);
            case 11 : return new DarkTower(b1, b2, b3);
            case 12 : return new LaserTower(b1, b2, b3);
            case 13 : return new GoldTower(b1, b2, b3);
            case 14 : return new LifeTower(b1, b2, b3);
            case 15 : return new DeathTower(b1, b2, b3);
            case 16 : return new InertiaTower(b1, b2, b3);
            case 17 : return new GlassTower(b1, b2, b3);
            case 18 : return new CopperTower(b1, b2, b3);
            case 19 : return new SnowTower(b1, b2, b3);
        }

        return null;
    }

    public static int getTowerCodeID(TowerCode tc)
    {
        if (tc is FireTower)
        {
            return 0;
        }
        if (tc is WaterTower)
        {
            return 1;
        }
        if (tc is IceTower)
        {
            return 2;
        }
        if (tc is LightningTower)
        {
            return 3;
        }
        if (tc is EarthTower)
        {
            return 4;
        }
        if (tc is IronTower)
        {
            return 5;
        }
        if (tc is AtomicTower)
        {
            return 6;
        }
        if (tc is SpaceTower)
        {
            return 7;
        }
        if (tc is WindTower)
        {
            return 8;
        }
        if (tc is ColorTower)
        {
            return 9;
        }
        if (tc is PoisonTower)
        {
            return 10;
        }
        if (tc is DarkTower)
        {
            return 11;
        }
        if (tc is LaserTower)
        {
            return 12;
        }
        if (tc is GoldTower)
        {
            return 13;
        }
        if (tc is LifeTower)
        {
            return 14;
        }
        if (tc is DeathTower)
        {
            return 15;
        }
        if (tc is InertiaTower)
        {
            return 16;
        }
        if (tc is GlassTower)
        {
            return 17;
        }
        if (tc is CopperTower)
        {
            return 18;
        }
        if (tc is SnowTower)
        {
            return 19;
        }
        return -1;
    }
}
