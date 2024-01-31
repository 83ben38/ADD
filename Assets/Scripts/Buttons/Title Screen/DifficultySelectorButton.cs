using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelectorButton : Selectable
{
    public DifficultyScriptableObject difficulty;
    private TextMeshPro tmp;
    private Material _material;

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
        SelectionData.data.difficulty = difficulty;
        SceneManager.LoadScene("Game");
    }

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        tmp = GetComponentInChildren<TextMeshPro>();
        tmp.text = difficulty.difficultyName;
    }
}
