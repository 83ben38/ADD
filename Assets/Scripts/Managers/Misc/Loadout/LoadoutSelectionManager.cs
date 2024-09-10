using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class LoadoutSelectionManager : MonoBehaviour
{
    public GameObject cloneObject;
    public static LoadoutButtonController[] currentButtons;
    public static LoadoutButtonController[][][] allButtons;
    public static int screen = 0;
    public static LoadoutSelectionManager manager;
    private void Start()
    {
        
        CreateMap();
        manager = this;
    }

    public void ResetMap()
    {
        for (int i = 0; i < allButtons.Length; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (allButtons[i][j] != null)
                {
                    for (int k = 0; k < allButtons[i][j].Length; k++)
                    {
                        allButtons[i][j][k].gameObject.SetActive(i == screen);
                    }
                }
            }
        }
    }

    void CreateMap()
    {
        int[] loadouts = SaveData.save.getAvailableLoadouts();
        allButtons = new LoadoutButtonController[(loadouts.Length + 9) / 10][][];
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i] = new LoadoutButtonController[10][];
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

                x %= 14;
                
                cloneObject.transform.position = new Vector3(x,0,y);
                
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

            allButtons[j / 10][j % 10] = buttons;
            if (loadouts[j] == SaveData.save.getLoadoutSelected())
            {
                currentButtons = buttons;
                screen = (j / 10);
            }
        }
        ResetMap();
        Destroy(cloneObject);
    }
}
