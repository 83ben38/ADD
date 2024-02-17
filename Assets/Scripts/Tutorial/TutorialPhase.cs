using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TutorialPhase
{
    [SerializeField]
    public GameObject[] disable;
    [SerializeField]
    public string[] text;
    [SerializeField]
    public GameObject[] enable;
}
