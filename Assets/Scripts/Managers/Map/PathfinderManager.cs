using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathfinderManager : MonoBehaviour
{
    public static PathfinderManager manager;
    public List<TowerController> tiles = new List<TowerController>();
    public List<TowerController> path = new List<TowerController>();
    public List<TowerController> starts = new List<TowerController>();
    public List<TowerController> ends = new List<TowerController>();
    public string shapeCode;
    private void Awake()
    {
        manager = this;
    }

    public void Run()
    {
        int max = SelectionData.data.map.xDimensions - 1;
        foreach (var tower in tiles)
        {
            if (tower.x == 0)
            {
                starts.Add(tower);
            }
            if (tower.x == max)
            {
                ends.Add(tower);
            }

            foreach (var tower2 in tiles)
            {
                if (NextToCheck.isNextTo(tower2.x, tower2.y, tower.x, tower.y, shapeCode))
                {
                    tower.nextTo.Add(tower2);
                }
            }
        }
        pathFind();
    }

    public bool pathFind()
    {
        
        foreach (var tower in tiles)
        {
            tower.minDist = 999;
        }

        List<TowerController> stack = new List<TowerController>();
        foreach (var tower in starts)
        {
            if (!tower.block)
            {
                stack.Add(tower);
                tower.minDist = 0;
            }
        }

        while (stack.Count > 0)
        {
            TowerController c = stack[0];
            stack.RemoveAt(0);
            foreach (var next in c.nextTo)
            {
                if (!next.block && next.minDist == 999 && !stack.Contains(next))
                {
                    next.minDist = c.minDist + 1;
                    stack.Add(next);
                }
            }
        }

        int mindist = 999;
        TowerController minTower = null;
        foreach (var tower in ends)
        {
            if (tower.minDist < mindist)
            {
                mindist = tower.minDist;
                minTower = tower;
            }
        }

        if (mindist < 998)
        {
            foreach (var tower in path)
            {
                if (!tower.block)
                {
                    tower.setBaseColor(false);
                }
            }

            path = new List<TowerController>();
            reversePathFind(minTower);
            return true;
        }
        
        return false;
    }

    

    private void reversePathFind(TowerController t)
    {
        path.Insert(0,t);
        t.setBaseColor(true);
        if (t.minDist == 0)
        {
            return;
        }

        foreach (var tower in t.nextTo)
        {
            if (t.minDist == tower.minDist + 1)
            {
                reversePathFind(tower);
                return;
            }
        }
    }
}
