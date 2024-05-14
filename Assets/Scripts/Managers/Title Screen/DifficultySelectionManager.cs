using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultySelectionManager : MonoBehaviour
{
    public static DifficultySelectionManager manager;
    public DifficultyScriptableObject[] availableDifficulties;
    public int xDimensions;
    public int yDimensions;
    public GameObject[][] stars;
    public GameObject[][] buttons;
    public GameObject buttonObject;
    public GameObject starObject;
    public BackButtonController backButton;

    private void Start()
    {
        manager = this;
    }

    public void end()
    {
        backButton.gameObject.SetActive(false);
        foreach (var someButtons in buttons)
        {
            foreach (var button in someButtons)
            {
                if (button != null)
                {
                    Destroy(button.gameObject);
                }
            }
        }
        foreach (var someStars in stars)
        {
            foreach (var star in someStars)
            {
                if (star != null)
                {
                    Destroy(star.gameObject);
                }
            }
        }
        
    }

    public void startDifficultySelection()
    {
        backButton.gameObject.SetActive(true);
        backButton.difficultyDisable = this;
        stars = new GameObject[xDimensions][];
            buttons = new GameObject[xDimensions][];
            for (int i = 0; i < xDimensions; i++)
            {
                stars[i] = new GameObject[yDimensions];
                buttons[i] = new GameObject[yDimensions];
            }

            {
                
                for (int i = 0; i < availableDifficulties.Length; i++)
                {
                    bool cont = false;
                    for (int k = 0; k < availableDifficulties[i].preRequisites.Length; k++)
                    {
                        if (!SaveData.save.isDifficultyCompleted(SelectionData.data.selectedMap, availableDifficulties[i].preRequisites[k]))
                        {
                            cont = true;
                            break;
                        }
                    }

                    stars[i / yDimensions][i % yDimensions] = Instantiate(starObject);
                    stars[i / yDimensions][i % yDimensions].transform.position = new Vector3(
                        ((i / yDimensions) * 1.5f) - 1,
                        4.5f + ((i % yDimensions) * 1.5f), (i % yDimensions) - 4);
                    buttons[i / yDimensions][i % yDimensions] = Instantiate(buttonObject);
                    buttons[i / yDimensions][i % yDimensions].transform.position =
                        stars[i / yDimensions][i % yDimensions].transform.position - new Vector3(0, 0.7f, 0);
                    buttons[i / yDimensions][i % yDimensions].GetComponent<DifficultySelectorButton>().difficulty =
                        availableDifficulties[i];
                    buttons[i / yDimensions][i % yDimensions].GetComponent<DifficultySelectorButton>().diffNum =
                        i;
                    stars[i / yDimensions][i % yDimensions].SetActive(true);
                    buttons[i / yDimensions][i % yDimensions].SetActive(true);
                    TextMeshPro tmp = stars[i / yDimensions][i % yDimensions].GetComponentInChildren<TextMeshPro>();
                    if (cont)
                    {
                        tmp.text = "Locked";
                    }
                    else
                    {
                        int money = SelectionData.data.map.baseAward * availableDifficulties[i].awardMultiplier;
                        if (!SaveData.save.isDifficultyCompleted(SelectionData.data.selectedMap, i))
                        {
                            money *= 3;
                        }

                        tmp.text = money + " \u20b5\u00a2";
                    }
                }
            }
       
            for (int i = 0; i < availableDifficulties.Length; i++)
            {
                stars[i / yDimensions][i % yDimensions].SetActive(true);
                buttons[i / yDimensions][i % yDimensions].SetActive(true);
            }
        
    }
}
