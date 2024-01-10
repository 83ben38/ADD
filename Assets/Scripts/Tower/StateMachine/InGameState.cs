using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InGameState : TowerState
{
    public static TowerCode held;

    public override void Run(TowerController controller)
    {
        
    }

    public static void generateNewTowerCode(int round)
    {
        int[] towerCodes = SelectionData.data.towerCodes;
        held = TowerCodeFactory.getTowerCode(towerCodes[Random.Range(0, towerCodes.Length)]);
        held.lvl = (int)Math.Sqrt(Random.Range(1, round));
    }

    public override void MouseClick(TowerController controller)
    {
        if (controller.wall)
        {
            return;
        }
        if (held != null)
        {
            if (controller.tower != null && !controller.tower.canMerge(held))
            {
                return;
            }

            if (controller.tower != null)
            {
                controller.tower.lvl++;
                controller.towerVisual.updateTower();
                held = null;
                return;
            }

            controller.block = true;
            if (PathfinderManager.manager.pathFind())
            {
                controller.StartCoroutine(BeforeGameState.changeTower(controller ,true));
                controller.setBaseColor(ColorManager.manager.tower,ColorManager.manager.towerHighlighted);
                controller.tower = held;
                controller.towerVisual.updateTower();
                held = null;
                return;
            }
            controller.block = false;
            return;
        }

        if (controller.tower != null)
        {
            held = controller.tower;
            controller.tower = null;
            controller.towerVisual.updateTower();
            controller.setBaseColor(ColorManager.manager.tile,ColorManager.manager.tileHighlighted);
            controller.block = false;
            PathfinderManager.manager.pathFind();
            controller.StartCoroutine(BeforeGameState.changeTower(controller ,false));
        }
    }

    
}
