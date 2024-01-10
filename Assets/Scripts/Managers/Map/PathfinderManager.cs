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
        foreach (var tower in tiles)
        {
            if (tower.x == 0)
            {
                starts.Add(tower);
            }
            if (tower.x == 9)
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

        foreach (var tower in starts)
        {
            if (!tower.block)
            {
                pathFind(tower, 0);
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
                    tower.setBaseColor(ColorManager.manager.tile, ColorManager.manager.tileHighlighted);
                }
            }

            path = new List<TowerController>();
            reversePathFind(minTower);
            return true;
        }
        
        return false;
    }

    private void pathFind(TowerController t, int n)
    {
        t.minDist = n;
        foreach (var tower in t.nextTo)
        {
            if (tower.minDist > n && !tower.block)
            {
                pathFind(tower, n + 1);
            }
        }
    }

    private void reversePathFind(TowerController t)
    {
        path.Insert(0,t);
        t.setBaseColor(ColorManager.manager.path,ColorManager.manager.pathHighlighted);
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
