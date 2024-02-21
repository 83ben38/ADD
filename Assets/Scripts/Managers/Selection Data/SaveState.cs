using UnityEditor;
using UnityEngine.Rendering;

[System.Serializable]
public class SaveState
{
    public int money;
    public int[] towersUnlocked;
    public SerializableDictionary<int,TripleBool> towerUpgradesAvailable;
    public SerializableDictionary<int,TripleBool> towerUpgradesEnabled;

    public int[] loadoutsUnlocked;

    public int loadoutSelected;

    public SerializableDictionary<int, SerializableDictionary<int, bool>> difficultiesCompleted;
    public bool[] tutorials;
    public SaveState()
    {
        money = 999999;
        //money = 0;
        towersUnlocked = new int[]{0,1,2,3,4};
        towerUpgradesAvailable = new SerializableDictionary<int, TripleBool>();
        towerUpgradesEnabled = new SerializableDictionary<int, TripleBool>();
        loadoutsUnlocked = new int[]{0};
        loadoutSelected = 0;
        //game, towers, shop, loadouts, maps/difficulties
        tutorials = new bool[] { false, false, false, false, false, false };
        for (int i = 0; i < 5; i++)
        {
            towerUpgradesAvailable[i] = new TripleBool(false,false,false);
            towerUpgradesEnabled[i] = new TripleBool(false,false,false);
        }
    }
    /*public SaveState()
    {
        money = 99999;
        towersUnlocked = new int[]{0,1,2,3,4,5,6,7,8,9};
        towerUpgradesAvailable = new SerializableDictionary<int,TripleBool>();
        towerUpgradesEnabled = new SerializableDictionary<int,TripleBool>();
        loadoutsUnlocked = new int[] { 0, 1, 2, 3, 4 };
        loadoutSelected = 0;
        for (int i = 0; i < 10; i++)
        {
            towerUpgradesAvailable[i] = new TripleBool( true, true, true);
            towerUpgradesEnabled[i] = new TripleBool(false, false, false);
        }

        difficultiesCompleted = new SerializableDictionary<int, SerializableDictionary<int, bool>>();
    }*/
}


