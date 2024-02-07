using UnityEngine.Rendering;

[System.Serializable]
public class SaveState
{
    public int money;
    public int[] towersUnlocked;
    public SerializedDictionary<int, bool[]> towerUpgradesAvailable;
    public SerializedDictionary<int, bool[]> towerUpgradesEnabled;

    public int[] loadoutsUnlocked;
    /*public SaveState()
    {
        money = 0;
        towersUnlocked = new int[]{0,1,2,3,4};
        towerUpgradesAvailable = new SerializedDictionary<int, bool[]>();
        towerUpgradesEnabled = new SerializedDictionary<int, bool[]>();
        loadoutsUnlocked = new int[]{0};
        for (int i = 0; i < 5; i++)
        {
            towerUpgradesAvailable[i] = new[] { false, false, false };
            towerUpgradesEnabled[i] = new[] { false, false, false };
        }
    }*/
    public SaveState()
    {
        money = 99999;
        towersUnlocked = new int[]{0,1,2,3,4,5,6,7};
        towerUpgradesAvailable = new SerializedDictionary<int, bool[]>();
        towerUpgradesEnabled = new SerializedDictionary<int, bool[]>();
        loadoutsUnlocked = new int[] { 0, 1, 2, 3, 4 };
        for (int i = 0; i < 8; i++)
        {
            towerUpgradesAvailable[i] = new[] { true, true, true };
            towerUpgradesEnabled[i] = new[] { false, false, false };
        }
    }
}
