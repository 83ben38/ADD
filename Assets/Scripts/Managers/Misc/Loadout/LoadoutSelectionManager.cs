using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class LoadoutSelectionManager : MonoBehaviour
{
    public GameObject cloneObject;
    public static LoadoutButtonController[] currentButtons;
    

    private void Start()
    {
        
        CreateMap();
       
    }

    void CreateMap()
    {
        int[] loadouts = SaveData.save.getAvailableLoadouts();
        for (int j = 0; j < loadouts.Length; j++)
        {
            int[] loadout = LoadoutManager.manager.loadouts[j].loadout;
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

            if (loadouts[j] == SaveData.save.getLoadoutSelected())
            {
                currentButtons = buttons;
            }
        }
        Destroy(cloneObject);
    }
}
