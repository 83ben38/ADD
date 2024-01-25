using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LivesController : MonoBehaviour
{
    public static LivesController controller;
    [SerializeField]
    private int lives;

    private Vector3 initialScale;
    private int maxLives;
    private TextMeshPro text;

    private void Start()
    {
        controller = this;
        initialScale = transform.localScale;
        maxLives = lives;
        text = GetComponentInChildren<TextMeshPro>();
        lives = SelectionData.data.difficulty.maxLives;
    }

    public void damage(int amount)
    {
        lives -= amount;
        transform.localScale = initialScale * lives / (float) maxLives;
        text.SetText("Lives:\n" + lives);
    }
}
