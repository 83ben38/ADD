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
    public WaveScriptableObject[] waves;
    private int wave = 0;
    public static bool waveGoing = false;
    public static bool waveFinished = true;
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
        if (waveFinished)
        {
            foreach (var tower in PathfinderManager.manager.tiles)
            {
                tower.state = tower.tower;
            }
            StartCoroutine(spawnWave());
        }
    }

    public IEnumerator spawnWave()
    {
        waveGoing = true;
        waveFinished = false;
        List<GameObject> objects = new List<GameObject>();
        StartCoroutine(waves[wave].spawnWaves(objects,this));
        while (waveGoing)
        {
            yield return null;
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
        waveFinished = true;
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
