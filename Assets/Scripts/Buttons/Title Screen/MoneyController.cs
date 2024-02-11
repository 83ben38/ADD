using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public TextMeshPro text;

    private void Start()
    {
        int money = SaveData.save.getMoney();
        if (money > 10000)
        {
            text.text = money / 1000 + "." + (money % 1000) / 100 + "K \u20b5\u00a2";
            return;
        }
        text.text = money+ " \u20b5\u00a2";
    }
}
