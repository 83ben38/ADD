using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveScriptableObject", order = 2)]
public class WaveScriptableObject : ScriptableObject
{
    public GameObject[] waves;
    public int[] waveNums;
    public float[] waveSpacings;
    public float[] waveDelays;

    private bool[] wavesFinished;
    
    public IEnumerator spawnWaves(List<GameObject> objects, StartButtonController c)
    {
        wavesFinished = new bool[waves.Length];
        for (int i = 0; i < waves.Length; i++)
        {
            c.StartCoroutine(spawnWave(i, objects));
        }

        while (wavesFinished.Contains(false))
        {
            yield return null;
        }

        StartButtonController.waveGoing = false;
    }

    public IEnumerator spawnWave(int waveNum, List<GameObject> objects)
    {
        for (float i = 0; i < waveDelays[waveNum]; i+=Time.deltaTime)
        {
            yield return null;
        }
        for (int i = 0; i < waveNums[waveNum]; i++)
        {
            objects.Add(Instantiate(waves[waveNum]));
            for (float j = 0; j < waveSpacings[waveNum]; j+=Time.deltaTime)
            {
                yield return null;
            }   
        }

        wavesFinished[waveNum] = true;
    }
}
