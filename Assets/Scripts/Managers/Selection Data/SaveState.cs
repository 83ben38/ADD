using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class SaveState
{
    public int money;
    public int[] towersUnlocked;
    public SerializedDictionary<int, bool[]> towerUpgradesAvailable;
    public SerializedDictionary<int, bool[]> towerUpgradesEnabled;

    public SaveState()
    {
        money = 0;
        towersUnlocked = new int[0];
        towerUpgradesAvailable = new SerializedDictionary<int, bool[]>();
        towerUpgradesEnabled = new SerializedDictionary<int, bool[]>();
    }
}
