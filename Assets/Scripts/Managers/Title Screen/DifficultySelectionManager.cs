using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        manager = this;
    }

    

    public void startDifficultySelection()
    {
        stars = new GameObject[xDimensions][];
        buttons = new GameObject[xDimensions][];
        for (int i = 0; i < xDimensions; i++)
        {
            stars[i] = new GameObject[yDimensions];
            buttons[i] = new GameObject[yDimensions];
        }

        for (int i = 0; i < availableDifficulties.Length; i++)
        {
            stars[i / yDimensions][i % yDimensions] = Instantiate(starObject);
            stars[i / yDimensions][i % yDimensions].transform.position = new Vector3(((i/yDimensions)*1.5f)-2, 3.5f+((i%yDimensions)*1.5f), (i%yDimensions)-4);
            stars[i / yDimensions][i % yDimensions].transform.rotation = Quaternion.Euler(((i%yDimensions)*-5)-55,0,0);
            buttons[i / yDimensions][i % yDimensions] = Instantiate(buttonObject);
            buttons[i / yDimensions][i % yDimensions].transform.position = stars[i / yDimensions][i % yDimensions].transform.position - new Vector3(-0.5f,0.25f,0);
            buttons[i / yDimensions][i % yDimensions].GetComponent<DifficultySelectorButton>().difficulty = availableDifficulties[i];
            stars[i / yDimensions][i % yDimensions].SetActive(true);
            buttons[i / yDimensions][i % yDimensions].SetActive(true);
        }
    }
}
