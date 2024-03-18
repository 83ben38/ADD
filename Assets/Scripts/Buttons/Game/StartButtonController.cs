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
    public bool tutorial = false;
    void Start()
    {
        waveGoing = false;
        waveFinished = true;
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
        if (waves.waves.Length == wave)
        {
            wave++;
            int money = waves.awardMultiplier * SelectionData.data.map.baseAward;
            if (!SaveData.save.isDifficultyCompleted(SelectionData.data.selectedMap, SelectionData.data.selectedDifficulty))
            {
                SaveData.save.completeDifficulty(SelectionData.data.selectedMap, SelectionData.data.selectedDifficulty);
                money *= 3;
            }

            SaveData.save.addMoney(money);
            WinController.controller.go(true);
            return;
        }
        if (waveFinished)
        {
            if (PathfinderManager.manager.path.Count == 0)
            {
                PathfinderManager.manager.pathFind();
            }

            foreach (var tower in PathfinderManager.manager.tiles)
            {
                tower.state = tower.tower;
                tower.tower?.roundStart();
            }
            StartCoroutine(spawnWave());
        }
    }

    public IEnumerator spawnWave()
    {
        if (waves.waves.Length < wave)
        {
            waveGoing = true;
            waveFinished = false;
            objects = new List<GameObject>();
            StartCoroutine(waves.freeplayWaves[wave%waves.freeplayWaves.Length].spawnWaves(objects, this,wave));
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
            if (tutorial)
            {
                tutorial = false;
                TutorialManager.manager.runNext();
            }
            waveFinished = true;
        }
    }

    public void go()
    {
        if (tutorial)
        {
            TutorialManager.manager.runNext();
        }
        text.SetText("Start");
        started = true;
        foreach (var tower in PathfinderManager.manager.tiles)
        {
            tower.state = new InGameState();
        }
        InGameState.generateNewTowerCode(1);
    }
}
