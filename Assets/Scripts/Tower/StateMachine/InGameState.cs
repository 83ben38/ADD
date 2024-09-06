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
        if (held.lvl > 6)
        {
            held.lvl = 6;
        }
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
                int lvl = held.lvl;
                controller.tower = controller.tower.merge(held);
                controller.tower.placedDown(controller);
                controller.towerVisual.updateTower();
                controller.setBaseColor(ColorManager.manager.tower,ColorManager.manager.towerHighlighted);
                if (controller.tower.lvl != lvl)
                {
                    held = null;
                }

                return;
            }

            controller.block = true;
            if (PathfinderManager.manager.pathFind())
            {
                controller.StartCoroutine(changeTower(controller ,true));
                

                controller.setBaseColor(ColorManager.manager.tower,ColorManager.manager.towerHighlighted);
                controller.tower = held;
                controller.tower.placedDown(controller);
                controller.towerVisual.updateTower();
                held = null;
                return;
            }
            controller.block = false;
            return;
        }

        if (controller.tower != null)
        {
            if (controller.tower is MultiTowerCode)
            {
                controller.tower.pickedUp();
            }

            held = controller.tower;
            held.pickedUp();
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
    public static IEnumerator changeTowerStatic(TowerController c, bool grow)
    {
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
    }
}
