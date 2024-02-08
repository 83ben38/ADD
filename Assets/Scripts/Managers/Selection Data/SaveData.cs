using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData save;
    [SerializeField]
    private SaveState state;
    private FileSaver saver;
    [SerializeField]
    private string path;
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
        path = Path.Combine(Application.persistentDataPath, "Save");
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
        state.towerUpgradesEnabled[tower][upgrade] = enabled;
        saver.Save(state);
    }

    public void setUpgradeAvailable(int tower, int upgrade, bool enabled)
    {
        state.towerUpgradesAvailable[tower][upgrade] = enabled;
        saver.Save(state);
    }

    public void setAvailableTower(int tower)
    {
        int[] towers = new int[state.towersUnlocked.Length + 1];
        state.towersUnlocked.CopyTo(towers, 0);
        towers[^1] = tower;
        state.towersUnlocked = towers;
        saver.Save(state);
    }

    public int[] getAvailableLoadouts()
    {
        return state.loadoutsUnlocked;
    }

    public void setAvailableLoadout(int loadout)
    {
        int[] loadouts = new int[state.loadoutsUnlocked.Length + 1];
        state.loadoutsUnlocked.CopyTo(loadouts, 0);
        loadouts[^1] = loadout;
        state.loadoutsUnlocked = loadouts;
        saver.Save(state);
    }

    public int getLoadoutSelected()
    {
        return state.loadoutSelected;
    }
    public void setLoadoutSelected(int loadout)
    {
        state.loadoutSelected = loadout;
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
