using UnityEngine.Rendering;

[System.Serializable]
public class SaveState
{
    public int money;
    public int[] towersUnlocked;
    public SerializableDictionary<int> towerUpgradesAvailable;
    public SerializableDictionary<int> towerUpgradesEnabled;

    public int[] loadoutsUnlocked;

    public int loadoutSelected;
    /*public SaveState()
    {
        money = 0;
        towersUnlocked = new int[]{0,1,2,3,4};
        towerUpgradesAvailable = new SerializableDictionary<int, bool[]>();
        towerUpgradesEnabled = new SerializableDictionary<int, bool[]>();
        loadoutsUnlocked = new int[]{0};
        loadoutSelected = 0;
        for (int i = 0; i < 5; i++)
        {
            towerUpgradesAvailable[i] = new[] { false, false, false };
            towerUpgradesEnabled[i] = new[] { false, false, false };
        }
    }*/
    public SaveState()
    {
        money = 99999;
        towersUnlocked = new int[]{0,1,2,3,4,5,6,7,8};
        towerUpgradesAvailable = new SerializableDictionary<int>();
        towerUpgradesEnabled = new SerializableDictionary<int>();
        loadoutsUnlocked = new int[] { 0, 1, 2, 3, 4 };
        loadoutSelected = 0;
        for (int i = 0; i < 9; i++)
        {
            towerUpgradesAvailable[i] = new TripleBool( true, true, true);
            towerUpgradesEnabled[i] = new TripleBool(false, false, false);
        }
    }
}
