using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerDescriptionManager : MonoBehaviour
{
    public TowerDescriptionScriptableObject[] descriptions;
    public GameObject tower;
    public TextMeshPro title;
    public TextMeshPro description;
    public TextMeshPro towerText;
    public int upgrade = 3;

    private void Start()
    {
        TowerDescriptionScriptableObject description = descriptions[SelectionData.data.towerSelected];
        TowerController tc = tower.GetComponent<TowerController>();
        tc.tower = TowerCodeFactory.getTowerCode(SelectionData.data.towerSelected);
        tc.tower.lvl = 1;
        Vector3 scale = tc.transform.localScale;
        tc.transform.localScale = new Vector3(scale.x, scale.y*2, scale.z);
        tc.towerVisual.updateTower();
        title.text = description.title;
        this.description.text = description.description;
        towerText.text = "Tower";
    }
}
