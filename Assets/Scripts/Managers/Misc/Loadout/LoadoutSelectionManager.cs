using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class LoadoutSelectionManager : MonoBehaviour
{
    public GameObject cloneObject;
    public static LoadoutButtonController[] currentButtons;
    public static LoadoutButtonController[][][] allButtons;
    public static LoadoutSelectionManager manager;
    public Quaternion rotation;
    private void Start()
    {
        
        CreateMap();
        manager = this;
    }


    void CreateMap()
    {
        int[] loadouts = SaveData.save.getAvailableLoadouts();
        allButtons = new LoadoutButtonController[(loadouts.Length+4) / 5][][];
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i] = new LoadoutButtonController[5][];
        }
        for (int j = 0; j < loadouts.Length; j++)
        {
            int[] loadout = LoadoutManager.manager.loadouts[loadouts[j]].loadout;
            LoadoutButtonController[] buttons = new LoadoutButtonController[loadout.Length];
            for (int i = 0; i < loadout.Length; i++)
            {
                int x = i;
                int y = 8-(j * 2);
                while (y < 0)
                {
                    y += 10;
                    x += 7;
                }

                cloneObject.transform.position = new Vector3((x/2.0f),(y/2.0f)-5,-2f);
                cloneObject.transform.rotation = rotation;
                cloneObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                buttons[i] = Instantiate(cloneObject).GetComponent<LoadoutButtonController>();
                buttons[i].text.text = loadout[i] + "";
                buttons[i].loadoutNum = loadouts[j];
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].other = buttons;
                if (loadouts[j] == SaveData.save.getLoadoutSelected())
                {
                    buttons[i].c1 = ColorManager.manager.tile;
                    buttons[i].c2 = ColorManager.manager.tileHighlighted;
                }
            }

            allButtons[j / 5][j % 5] = buttons;
            if (loadouts[j] == SaveData.save.getLoadoutSelected())
            {
                currentButtons = buttons;
            }
        }

        Destroy(cloneObject);
    }
}
