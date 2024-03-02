using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Renderer>().material.color = ColorManager.manager.background;
    }

    
}
