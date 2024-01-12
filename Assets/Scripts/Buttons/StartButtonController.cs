using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;

public class StartButtonController : Selectable
{
    private Material _material;
    public TextMeshPro text;
    private bool started = false;
    public GameObject[] waves;
    public int[] waveNums;
    public int[] waveSpacings;
    private int wave = 0;
    private bool waveGoing = false;
    void Start()
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
        //start
        if (started)
        {
            start();
        }
        else
        {
            go();
        }
    }

    public void start()
    {
        if (!waveGoing)
        {
            foreach (var tower in PathfinderManager.manager.tiles)
            {
                tower.state = tower.tower;
            }
            StartCoroutine(spawnWave(wave));
        }
    }

    public IEnumerator spawnWave(int wave)
    {
        waveGoing = true;
        List<GameObject> objects = new List<GameObject>();
        for (int i = 0; i < waveNums[wave]; i++)
        {
            objects.Add(Instantiate(waves[wave]));
            for (int j = 0; j < waveSpacings[wave]; j++)
            {
                yield return null;
            }   
        }
        
        while (objects.Count > 0)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == null)
                {
                    objects.RemoveAt(i);
                    i--;
                }
            }

            yield return null;
        }
        foreach (var tower in PathfinderManager.manager.tiles)
        {
            tower.state = new InGameState();
        }
        wave++;
        InGameState.generateNewTowerCode(wave+1);
        waveGoing = false;
    }

    public void go()
    {
        text.SetText("Start");
        started = true;
        foreach (var tower in PathfinderManager.manager.tiles)
        {
            tower.state = new InGameState();
        }
        InGameState.generateNewTowerCode(1);
    }
}
