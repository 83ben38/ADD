using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerGhostController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] children = new GameObject[7];
    public Shape shape;
    private TowerController current;
    [SerializeField]
    private GameObject disable;
    
    private void Start()
    {
        shape = disable.GetComponentInChildren<Shape>();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].transform.localPosition = shape.getDotPos(i);
        }
        disable.SetActive(false);
        Color co = ColorManager.manager.tower;
        disable.GetComponentInChildren<Renderer>().material.color = new Color(co.r,co.g,co.b,0.4f);
        transform.localScale = new Vector3(transform.localScale.x, (float)(transform.localScale.y * Math.Sqrt(2)), transform.localScale.z) * MapCreator.scale;
    }

    private void Update()
    {
        if (StartButtonController.waveFinished)
        {
            

            if (MouseManager.manager.mouseOn is TowerController)
            {
                TowerController t = MouseManager.manager.mouseOn as TowerController;
                if (t == current)
                {
                    return;
                }
                if (InGameState.held == null)
                {
                    disable.SetActive(false);
                    return;
                }
                current = t;
                transform.position = t.transform.position;
                transform.rotation = t.transform.rotation;
                if (!t.block)
                {
                    updateTower(InGameState.held);
                }
                else if (t.tower != null)
                {
                    if (InGameState.held != null)
                    {
                        if (t.tower.canMerge(InGameState.held))
                        {
                            InGameState.held.lvl++;
                            updateTower(InGameState.held);
                            InGameState.held.lvl--;
                        }
                    }
                }
                else
                {
                    disable.SetActive(false);
                }
            }
            else
            {
                if (current != null)
                {
                    current = null;
                    disable.SetActive(false);
                }
            }
        }
        else
        {
            disable.SetActive(false);
        }
    }

    public void updateTower(TowerCode c)
    {
        if (c == null)
        {
            disable.SetActive(false);
        }
        disable.SetActive(true);
        foreach (var child in children)
        { 
            child.SetActive(false);
            
        }
        if (c == null)
        {
            return;
        }
        
        Color co = c.getColor();
        setColor(new Color(co.r,co.g,co.b,0.4f));
        int z = c.lvl;
        if (z % 2 == 1)
        {
            children[0].SetActive(true);
        }

        if (z > 1)
        {
            children[2].SetActive(true);
            children[5].SetActive(true);
        }
        if (z > 3)
        {
            children[1].SetActive(true);
            children[4].SetActive(true);
        }
        if (z > 5)
        {
            children[3].SetActive(true);
            children[6].SetActive(true);
        }
    }
    

    public void setColor(Color co)
    {
        foreach (var child in children)
        {
            child.GetComponent<Renderer>().material.color = co;
        }
    }
}
