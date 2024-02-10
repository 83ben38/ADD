using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InGameState : TowerState
{
    public static TowerCode held;
    private bool coroutine = false;
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
        if (controller.wall || coroutine || !controller.editable)
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
                controller.tower = controller.tower.merge(held);
                controller.towerVisual.updateTower();
                held = null;
                return;
            }

            controller.block = true;
            if (PathfinderManager.manager.pathFind())
            {
                controller.StartCoroutine(changeTower(controller ,true));
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
            controller.setBaseColor(false);
            controller.block = false;
            PathfinderManager.manager.pathFind();
            controller.StartCoroutine(changeTower(controller ,false));
        }
    }

    public IEnumerator changeTower(TowerController c, bool grow)
    {
        coroutine = true;
        Vector3 scale = c.transform.localScale;
        if (!grow)
        {
            scale.y /= 2;
        }

        for (float i = 0; i < 0.25f; i+=Time.deltaTime)
        {
            
            c.transform.localScale = new Vector3(scale.x,scale.y*(1 + (grow ? i*4 : (0.25f-i)*4)), scale.z);
            yield return null;
        } 
        
        c.transform.localScale = new Vector3(scale.x, scale.y*(grow ? 2 : 1), scale.z);
        coroutine = false;
    }
}
