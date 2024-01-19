using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO: ADD HIGHLIGHTED BUTTONS USE YOUTUBE 
public class PlayButtonScript : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject GoButton;
    public GameObject SettingsButton;
    public GameObject ExitButton;

    private void Start()
    {
        GoButton.SetActive(false);
        SettingsButton.SetActive(false);
        ExitButton.SetActive(false);

    }
    
    public void ButtonPressed()
    {
        Debug.Log("IT SHOULD WORK");
        GoButton.SetActive(true);
        SettingsButton.SetActive(true);
        ExitButton.SetActive(true);
        StartButton.SetActive(false);
        
    }

    
}

