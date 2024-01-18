using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.UIElements;

public class BeforeGameState : TowerState
{
    private bool coroutine = false;
    public override void Run(TowerController c)
    {

    }

    public override void MouseClick(TowerController c)
    {
        if (coroutine || !c.editable)
        {
            return;
        }

        if (!c.wall)
        {
            c.wall = true;
            c.block = true;
            if (PathfinderManager.manager.pathFind())
            
            {
                c.setBaseColor(ColorManager.manager.wall, ColorManager.manager.wallHighlighted);
                c.StartCoroutine(changeTower(c, true));
                
            }
            else
            {
                c.wall = false;
                c.block = false;
            }

            return;
        }
        c.setBaseColor(ColorManager.manager.tile,ColorManager.manager.tileHighlighted);
        c.wall = false;
        c.block = false;
        PathfinderManager.manager.pathFind();
        c.StartCoroutine(changeTower(c, false));
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
