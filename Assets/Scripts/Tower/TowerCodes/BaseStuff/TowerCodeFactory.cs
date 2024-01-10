using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCodeFactory : MonoBehaviour
{
    public static TowerCode getTowerCode(int i)
    {
        switch (i)
        {
            case 0 : return new FireTower();
            case 1 : return new WaterTower();
            case 2 : return new IceTower();
            case 3 : return new LightningTower();
            case 4 : return new EarthTower();
        }

        return null;
    }
}