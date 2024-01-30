using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectionManager : MonoBehaviour
{
    public static MapSelectionManager manager;
    public GameObject[] arrowButtons;
    public MapScriptableObject[] availableMaps;
    private GameObject[][][] maps;

    private void Start()
    {
        manager = this;
    }

    public void startMapSelection()
    {
        foreach (var button in arrowButtons)
        {
            button.SetActive(true);
        }

        maps = new GameObject[(availableMaps.Length + 5) / 6][][];
        for (int i = 0; i < (availableMaps.Length + 5) / 6; i++)
        {
            maps[i] = new[]
            {
                new GameObject[3],
                new GameObject[3]
            };
        }
        for (int i = 0; i < availableMaps.Length; i++)
        {

            maps[i / 6][i % 2][(i % 6) / 2] = new GameObject(availableMaps[i].mapName + " MiniMap",  typeof(MiniMap));
            maps[i / 6][i % 2][(i % 6) / 2].GetComponent<MiniMap>().setUp(availableMaps[i]);
            maps[i / 6][i % 2][(i % 6) / 2].transform.position = new Vector3(i, 0, 0);
        }
    }
}
