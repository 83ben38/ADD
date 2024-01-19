using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressGo : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonPressed()
    {
        Debug.Log("SHOULD SWITCH SCENE");
        SceneManager.LoadScene("Game");

    }
}
