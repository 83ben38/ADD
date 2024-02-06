using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : Selectable
{
    public Material _material;
    public int upgradeNum;
    private int tower;
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        tower = SelectionData.data.towerSelected;
        if (upgradeNum == 3)
        {
            _material.color = ColorManager.manager.tile;
            return;
        }

        _material.color = SaveData.save.isUpgradeEnabled(tower, upgradeNum) ? ColorManager.manager.tile : ColorManager.manager.path;
    }

    public override void MouseEnter()
    {
        if (upgradeNum == 3)
        {
            _material.color = ColorManager.manager.tileHighlighted;
            return;
        }
        _material.color = SaveData.save.isUpgradeEnabled(tower,upgradeNum) ? ColorManager.manager.tileHighlighted : ColorManager.manager.pathHighlighted;
    }

    public override void MouseExit()
    {
        if (upgradeNum == 3)
        {
            _material.color = ColorManager.manager.tile;
            return;
        }
        _material.color = SaveData.save.isUpgradeEnabled(tower, upgradeNum) ? ColorManager.manager.tile : ColorManager.manager.path;
    }

    public override void MouseClick()
    {
        TowerDescriptionManager.manager.setUpgrade(upgradeNum,this);
    }
}
