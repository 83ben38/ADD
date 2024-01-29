using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonController : Selectable
{
    public GameObject title;
    public List<GameObject> enable;
    private Material _material;
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
        foreach (GameObject obj in enable)
        {
            obj.SetActive(true);
        }
        title.SetActive(false);
        gameObject.SetActive(false);
    }
}
