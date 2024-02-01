using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class LivesController : MonoBehaviour
{
    public static LivesController controller;
    [SerializeField]
    private int lives;

    public Vector3 minScale;
    private Vector3 initialScale;
    private int maxLives;
    private TextMeshPro text;

    private void Start()
    {
        controller = this;
        initialScale = transform.localScale;
        lives = SelectionData.data.difficulty.maxLives;
        maxLives = lives;
        text = GetComponentInChildren<TextMeshPro>();
        
    }

    public void damage(int amount)
    {
        lives -= amount;
        transform.localScale = ((initialScale-minScale) * lives / (float) maxLives)+minScale;
        text.SetText("Lives:\n" + lives);
        if (lives < 1)
        {
            WinController.controller.go(false);
        }
    }
}
