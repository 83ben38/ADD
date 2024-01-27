using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;

public class StartButtonController : Selectable
{
    public static StartButtonController startButton;
    private Material _material;
    public TextMeshPro text;
    private bool started = false;
    public DifficultyScriptableObject waves;
    private int wave = 0;
    public static bool waveGoing = false;
    public static bool waveFinished = true;
    public List<GameObject> objects;
    void Start()
    {
        waves = SelectionData.data.difficulty;
        _material = GetComponent<Renderer>().material;
        _material.color = ColorManager.manager.tile;
        startButton = this;
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
       
        if (waves.waves.Length == wave)
        {
            SaveData.save.addMoney(waves.awardMultiplier * SelectionData.data.map.baseAward);
            WinController.controller.go(true);
        }
        else
        {
            waveGoing = true;
            waveFinished = false;
            objects = new List<GameObject>();
            StartCoroutine(waves.waves[wave].spawnWaves(objects, this));
            while (waveGoing)
            {
                yield return null;
            }

            while (objects.Count > 0)
            {
                yield return null;
            }

            foreach (var tower in PathfinderManager.manager.tiles)
            {
                tower.state = new InGameState();
            }

            wave++;
            InGameState.generateNewTowerCode(wave + 1);
            waveFinished = true;
        }
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
