using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionData : MonoBehaviour
{
    public static SelectionData data;

    public int[] towerCodes = {0,1,2,3,4};
    // Start is called before the first frame update
    void Start()
    {
        if (data != null)
        {
            Destroy(this);
        }
        data = this;
        DontDestroyOnLoad(gameObject);
    }
}
