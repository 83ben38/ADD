using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionData : MonoBehaviour
{
    public static SelectionData data;
    public MapScriptableObject map;
    public DifficultyScriptableObject difficulty;

    public int[] towerCodes = {5};
    // Start is called before the first frame update
    void OnEnable()
    {
        if (data != null)
        {
            Destroy(this);
        }
        data = this;
        DontDestroyOnLoad(gameObject);
    }
}
