using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class BeforeGameState : TowerState
{

    public override void Run(TowerController c)
    {

    }

    public override void MouseClick(TowerController c)
    {
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

    public static IEnumerator changeTower(TowerController c, bool grow)
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 scale = c.transform.localScale;
            c.transform.localScale = new Vector3(scale.x,scale.y + (grow ? .02f : -.02f), scale.z);
            yield return null;
        }
    }
}
