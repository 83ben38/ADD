using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectionManager : MonoBehaviour
{
    public static MapSelectionManager manager;
    public BackButtonController backButton;
    public MapScriptableObject[] availableMaps;
    public GameObject[][][] maps;
    public GameObject[][][] buttons;
    public GameObject buttonObject;

    private void Start()
    {
        manager = this;
    }

    public void setEnabled()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            foreach (var two in maps[i])
            {
                foreach (var one in two)
                {
                    if (one != null)
                    {
                        one.SetActive(true);
                    }
                }
            }
            foreach (var two in buttons[i])
            {
                foreach (var one in two)
                {
                    if (one != null)
                    {
                        one.SetActive(true);
                    }
                }
            }
        }
    }

    public void end()
    {
        backButton.gameObject.SetActive(false);
        for (int i = 0; i < maps.Length; i++)
        {
            foreach (var two in maps[i])
            {
                foreach (var one in two)
                {
                    if (one != null)
                    {
                        one.SetActive(false);
                    }
                }
            }
            foreach (var two in buttons[i])
            {
                foreach (var one in two)
                {
                    if (one != null)
                    {
                        one.SetActive(false);
                    }
                }
            }
        }
    }

    public void startMapSelection()
    {
        backButton.gameObject.SetActive(true);
        backButton.mapDisable = this;
        

        if (maps == null)
        {
            maps = new GameObject[(availableMaps.Length + 5) / 6][][];
            buttons = new GameObject[(availableMaps.Length + 5) / 6][][];
            for (int i = 0; i < (availableMaps.Length + 5) / 6; i++)
            {
                maps[i] = new[]
                {
                    new GameObject[3],
                    new GameObject[3]
                };
                buttons[i] = new[]
                {
                    new GameObject[3],
                    new GameObject[3]
                };
            }

            for (int i = 0; i < availableMaps.Length; i++)
            {

                maps[i / 6][i % 2][(i % 6) / 2] =
                    new GameObject(availableMaps[i].mapName + " MiniMap", typeof(MiniMap));
                maps[i / 6][i % 2][(i % 6) / 2].GetComponent<MiniMap>().setUp(availableMaps[i]);
                maps[i / 6][i % 2][(i % 6) / 2].transform.position = new Vector3(((((i % 6) / 2) + (i/6)*3) * 1.5f) - 2,
                    -5f + ((i % 2) * 1.5f), -4);
                maps[i / 6][i % 2][(i % 6) / 2].transform.rotation = Quaternion.Euler(90, 0, 0);
                buttons[i / 6][i % 2][(i % 6) / 2] = Instantiate(buttonObject);
                buttons[i / 6][i % 2][(i % 6) / 2].transform.position =
                    maps[i / 6][i % 2][(i % 6) / 2].transform.position - new Vector3(-.5f, 1f, 0);
                buttons[i / 6][i % 2][(i % 6) / 2].GetComponent<MapSelectorButton>().map = availableMaps[i];
                buttons[i / 6][i % 2][(i % 6) / 2].GetComponent<MapSelectorButton>().mapNum = i;
            }
        }

        setEnabled();
    }
}
