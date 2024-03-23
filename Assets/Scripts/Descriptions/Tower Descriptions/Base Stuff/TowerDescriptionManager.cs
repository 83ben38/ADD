using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerDescriptionManager : MonoBehaviour
{
    public static TowerDescriptionManager manager;
    public TowerDescriptionScriptableObject[] descriptions;
    public TowerDescriptionScriptableObject description;
    public GameObject tower;
    public TextMeshPro title;
    public TextMeshPro descriptionText;
    public TextMeshPro towerText;
    public TextMeshPro enabledText;
    public UpgradeButton currentButton;
    public EnabledButton enabledButton;
    public int upgrade = 3;
    public bool upgradeEnabled;

    private void Start()
    {
        manager = this;
        description = descriptions[SelectionData.data.towerSelected];
        TowerController tc = tower.GetComponent<TowerController>();
        tc.state = new EmptyState();
        tc.tower = TowerCodeFactory.getTowerCode(SelectionData.data.towerSelected);
        tc.tower.lvl = 1;
        Vector3 scale = tc.transform.localScale;
        tc.transform.localScale = new Vector3(scale.x, scale.y*2, scale.z);
        tc.towerVisual.updateTower();
        title.text = description.title;
        descriptionText.text = description.description;
        towerText.text = "Tower";
        enabledButton.gameObject.SetActive(false);
    }

    public void setEnabled()
    {
        if (SaveData.save.isUpgradeAvailable(SelectionData.data.towerSelected, upgrade))
        {
            upgradeEnabled = !upgradeEnabled;
            SaveData.save.setUpgradeEnabled(SelectionData.data.towerSelected, upgrade, upgradeEnabled);
            enabledButton.c1 = upgradeEnabled ? ColorManager.manager.tile: ColorManager.manager.path;
            enabledButton.c2 = upgradeEnabled ? ColorManager.manager.tileHighlighted: ColorManager.manager.pathHighlighted;
            currentButton._material.color = enabledButton.c1;
            enabledButton._material.color = enabledButton.c2;
            enabledText.text = upgradeEnabled ? "Enabled" : "Disabled";
        }
    }

    public void setUpgrade(int num, UpgradeButton nextButton)
    {
        currentButton = nextButton;
        upgrade = num;
        if (num != 3)
        {
            enabledButton.gameObject.SetActive(true);
            upgradeEnabled = SaveData.save.isUpgradeEnabled(SelectionData.data.towerSelected, num);
            enabledButton.c1 = upgradeEnabled ? ColorManager.manager.tile: ColorManager.manager.path;
            enabledButton.c2 = upgradeEnabled ? ColorManager.manager.tileHighlighted: ColorManager.manager.pathHighlighted;
            enabledText.text = upgradeEnabled ? "Enabled" : "Disabled";
            if (!SaveData.save.isUpgradeAvailable(SelectionData.data.towerSelected, num))
            {
                enabledText.text = "Locked";
            }

            towerText.text = "Upgrade " + (num+1);
            enabledButton._material.color = enabledButton.c1;
        }
        else
        {
            enabledButton.gameObject.SetActive(false);
            towerText.text = "Tower";
        }

        switch (num)
        {
            case 0 : descriptionText.text = description.upgrade1Description;
                break;
            case 1 : descriptionText.text = description.upgrade2Description;
                break;
            case 2 : descriptionText.text = description.upgrade3Description;
                break;
            case 3 : descriptionText.text = description.description;
                break;
        }
    }
}
