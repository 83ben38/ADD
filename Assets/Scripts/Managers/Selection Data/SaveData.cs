using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData save;
    [SerializeField]
    private SaveState state;
    private FileSaver saver;
    [SerializeField]
    private string path;
    private void OnEnable()
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
        return state.towerUpgradesAvailable.ContainsKey(tower) && state.towerUpgradesAvailable[tower][upgrade];
    }

    public bool isUpgradeEnabled(int tower, int upgrade)
    {
        return state.towerUpgradesAvailable.ContainsKey(tower) &&state.towerUpgradesEnabled[tower][upgrade];
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
        state.towerUpgradesEnabled[tower] = new TripleBool(false, false, false);
        state.towerUpgradesAvailable[tower] = new TripleBool(false, false, false);
        saver.Save(state);
    }

    public bool isDifficultyCompleted(int map, int difficulty)
    {
        if (!state.difficultiesCompleted.ContainsKey(map)) return false;
        if (!state.difficultiesCompleted[map].ContainsKey(difficulty)) return false; 
        return state.difficultiesCompleted[map][difficulty];
    }

    public void completeDifficulty(int map, int difficulty)
    {
        if (!state.difficultiesCompleted.ContainsKey(map))
            state.difficultiesCompleted[map] = new SerializableDictionary<int, bool>();
        state.difficultiesCompleted[map][difficulty] = true;
        saver.Save(state);
    }

    public int[] getAvailableLoadouts()
    {
        return state.loadoutsUnlocked;
    }

    public bool isTutorialCompleted(int tutorialNum)
    {
        return state.tutorials[tutorialNum];
    }

    public void completeTutorialPhase(int tutorialNum)
    {
        state.tutorials[tutorialNum] = true;
        saver.Save(state);
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
