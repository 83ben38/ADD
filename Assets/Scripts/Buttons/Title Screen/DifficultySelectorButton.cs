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
        _material.color = difficulty.difficultyColorHighlighted;
    }

    public override void MouseExit()
    {
        _material.color = difficulty.difficultyColor;
    }

    public override void MouseClick()
    {
        SelectionData.data.difficulty = difficulty;
        SceneManager.LoadScene("Tower Selection");
    }

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        tmp = GetComponentInChildren<TextMeshPro>();
        tmp.text = difficulty.difficultyName;
        _material.color = difficulty.difficultyColor;
    }
}
