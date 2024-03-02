using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowersButtonController : Selectable
{
    private Material _material;
    public string scene;
    
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = ColorManager.manager.tile;
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
        SceneManager.LoadScene(scene);
    }
}
