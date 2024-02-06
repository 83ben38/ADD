using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData save;

    private void Start()
    {
        if (save == null)
        {
            save = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool isUpgradeAvailable(int tower, int upgrade)
    {
        return true;
    }

    public bool isUpgradeEnabled(int tower, int upgrade)
    {
        return true;
    }

    public void setUpgradeEnabled(int tower, int upgrade, bool enabled)
    {
        
    }

    public int[] getAvailableTowers()
    {
        return new int[] {0,1,2,3,4,5,6,7 };
    }

    public int getMoney()
    {
        return 0;
    }

    public void addMoney(int money)
    {
        
    }
}
