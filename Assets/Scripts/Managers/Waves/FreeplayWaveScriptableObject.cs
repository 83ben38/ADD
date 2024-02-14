using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FreeplayWaveScriptableObject", order = 8)]
public class FreeplayWaveScriptableObject : MonoBehaviour
{
     public GameObject[] waves;
    public float[] defaultWaveNums;
    public float[] scalingWaveNums;
    public float[] waveSpacings;
    public float[] waveDelays;
    private bool[] wavesFinished;
    public FreeplayEnemyConfiguration[] configs;
    private int increase;
    
    public IEnumerator spawnWaves(List<GameObject> objects, StartButtonController c, int increase)
    {
        this.increase = increase;
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
        int numPaths = PathfinderManager.manager.numPaths;
        int currrentPath = 0;
        for (float i = 0; i < waveDelays[waveNum]; i+=Time.deltaTime)
        {
            yield return null;
        }
        for (int i = 0; i < defaultWaveNums[waveNum]+(scalingWaveNums[waveNum]*increase); i++)
        {

            GameObject go = Instantiate(waves[waveNum]);
            FruitCode fc =go.GetComponent<FruitCode>();
            configs[waveNum].runOptions(fc,increase);
            fc.path = currrentPath;
            Vector3 v = PathfinderManager.manager.path[currrentPath][0].transform.position;
            fc.goalPos = PathfinderManager.manager.path[currrentPath][1].transform.position;
            fc.transform.position = new Vector3(v.x, v.y + MapCreator.scale, v.z);
            currrentPath++;
            if (currrentPath >= numPaths)
            {
                currrentPath = 0;
            }

            objects.Add(go);
            for (float j = 0; j < waveSpacings[waveNum]; j+=Time.deltaTime)
            {
                yield return null;
            }   
        }

        wavesFinished[waveNum] = true;
    }
}
