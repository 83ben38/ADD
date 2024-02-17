using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonController : Selectable
{
    private Material _material;
    public MapScriptableObject tutorialMap;
    public DifficultyScriptableObject tutorialDifficulty;
    
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }
    public override void MouseEnter()
    {
        _material.color = ColorManager.manager.tileHighlighted;
    }

    public override void MouseExit()
    {
        _material.color = ColorManager.manager.tile;
    }

    public override void MouseClick()
    {
        if (SaveData.save.getTutorialPhase() == 0)
        {
            SelectionData.data.towerCodes = new[] { 0 };
            SelectionData.data.map = tutorialMap;
            SelectionData.data.selectedMap = -1;
            SelectionData.data.selectedDifficulty = -1;
            SelectionData.data.difficulty = tutorialDifficulty;
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Title Screen");
        }
    }
}
