using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager manager;
    public LoadoutScriptableObject[] loadouts;

    private void Awake()
    {
        if (manager != null)
        {
            Destroy(this);
            return;
        }

        manager = this;
        DontDestroyOnLoad(gameObject);
    }
}
