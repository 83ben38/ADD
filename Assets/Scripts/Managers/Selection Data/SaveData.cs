using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData save;
    public SaveState state;
    private FileSaver saver;

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

        saver = new FileSaver(Application.persistentDataPath, "Save");
        state = saver.Load();
        if (state == null)
        {
            state = new SaveState();
            saver.Save(state);
        }
    }

    public bool isUpgradeAvailable(int tower, int upgrade)
    {
        return state.towerUpgradesAvailable[tower][upgrade];
    }

    public bool isUpgradeEnabled(int tower, int upgrade)
    {
        return state.towerUpgradesEnabled[tower][upgrade];
    }

    public void setUpgradeEnabled(int tower, int upgrade, bool enabled)
    {
        saver.Save(state);
    }

    public int[] getAvailableTowers()
    {
        return state.towersUnlocked;
    }

    public int getMoney()
    {
        return state.money;
    }

    public void addMoney(int money)
    {
        state.money += money;
        saver.Save(state);
    }
}
