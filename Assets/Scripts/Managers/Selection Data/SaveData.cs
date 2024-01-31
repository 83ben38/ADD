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

    public int[] getAvailableTowers()
    {
        return new int[] {0,1,2,3,4,5,6 };
    }

    public int getMoney()
    {
        return 0;
    }

    public void addMoney(int money)
    {
        
    }
}
