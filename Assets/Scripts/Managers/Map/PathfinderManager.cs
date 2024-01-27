using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;
using Vector2 = System.Numerics.Vector2;

public class PathfinderManager : MonoBehaviour
{
    public static PathfinderManager manager;
    public List<TowerController> tiles = new List<TowerController>();
    public List<List<TowerController>> path = new List<List<TowerController>>();
    public List<TowerController> starts = new List<TowerController>();
    public List<TowerController> ends = new List<TowerController>();
    public List<TowerController> checkpoints = new List<TowerController>();
    public int numPaths;
    public int numCheckpoints;
    public string shapeCode;
    private void Awake()
    {
        manager = this;
    }

    public void Run()
    {
        foreach (var tower in tiles)
        {
            

            foreach (var tower2 in tiles)
            {
                if (NextToCheck.isNextTo(tower2.x, tower2.y, tower.x, tower.y, shapeCode))
                {
                    tower.nextTo.Add(tower2);
                }

                if (tower2.tileType > 2 && tower2.tileType == tower.tileType)
                {
                    tower.nextTo.Add(tower2);
                }
            }
        }
    }

    public void pathFind(TowerController start)
    {
        foreach (var tower in tiles)
        {
            tower.minDist = 999;
        }

        List<TowerController> stack = new List<TowerController>();
        stack.Add(start);
        start.minDist = 0;

        while (stack.Count > 0)
        {
            TowerController c = stack[0];
            stack.RemoveAt(0);
            foreach (var next in c.nextTo)
            {
                
                if (!next.block && next.minDist == 999 && !stack.Contains(next))
                {
                    if (next.tileType > 2 && next.tileType == c.tileType)
                    {
                        next.minDist = c.minDist;
                        stack.Insert(0,next);
                    }
                    else
                    {
                        next.minDist = c.minDist + 1;
                        stack.Add(next);
                    }
                }
            }
        }

    }
    public void pathFindPear(TowerController start)
    {
        foreach (var tower in tiles)
        {
            tower.minDist = 999;
        }

        List<TowerController> stack = new List<TowerController>();
        stack.Add(start);
        start.minDist = 0;

        while (stack.Count > 0)
        {
            TowerController c = stack[0];
            stack.RemoveAt(0);
            foreach (var next in c.nextTo)
            {
                
                if ((!(next.block&&c.block)) && next.tower == null && next.minDist == 999)
                {
                    
                    if (next.tileType > 2 && next.tileType == c.tileType)
                    {
                        next.minDist = c.minDist;
                    }
                    else if (next.block)
                    {
                        next.minDist = c.minDist + 3;
                    }
                    else
                    {
                        next.minDist = c.minDist + 1;
                    }

                    for (int i = 0; i <= stack.Count; i++)
                    {
                        if (i == stack.Count)
                        {
                            stack.Add(next);
                            break;
                        }

                        if (stack[i].minDist > next.minDist)
                        {
                            stack.Insert(i,next);
                            break;
                        }
                    }
                }
            }
        }

    }

    /*public bool pathFind()
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
    }*/

    public List<TowerController> requestPearPath(TowerController start)
    {
        Hashtable nodes = new Hashtable();
            pathFind(start);
            nodes.Add(start,new Node());
            Node n = (Node)nodes[start];
            n.tower = start;
            foreach (var check in checkpoints)
            {
                
                if (check.minDist < 999)
                {
                    if (!nodes.Contains(check))
                    {
                        nodes.Add(check,new Node());
                    }
                    Node c = (Node)nodes[check];
                    c.tower = check;
                    n.connecetions.Add(c,check.minDist);
                }
            }

            int minDistToEnd = 999;
            foreach (var end in ends)
            {
                
                if (end.minDist < minDistToEnd)
                {
                    minDistToEnd = end.minDist;
                }
            }

            n.distToEnd = minDistToEnd;
        foreach (var begin in checkpoints)
        {
            pathFindPear(begin);
            if (!nodes.Contains(begin))
            {
                nodes.Add(begin,new Node());
            }
            n = (Node)nodes[begin];
            n.tower = begin;
            foreach (var check in checkpoints)
            {
                
                if (check != begin &&check.minDist < 999)
                {
                    if (!nodes.Contains(check))
                    {
                        nodes.Add(check,new Node());
                    }

                    Node c = (Node)nodes[check];
                    c.tower = check;
                    n.connecetions.Add(c,check.minDist);
                }
            }

            minDistToEnd = 999;
            foreach (var end in ends)
            {
                
                if (end.minDist < minDistToEnd)
                {
                    minDistToEnd = end.minDist;
                }
            }

            n.distToEnd = minDistToEnd;
        }

        for (int i = 0; i < 2; i++)
        {
            foreach (Node node in nodes.Values)
            {
                node.collapse();
            }
        }
        List<TowerController> path = new List<TowerController>(); 
        Node current = (Node)nodes[start]; 
        current.getMinDist(new List<Node>(), numCheckpoints); 
        Node next = current.nextNode; 
        while (next != null) 
        { 
            List<TowerController> pathSection = new List<TowerController>(); 
            pathFindPear(current.tower); 
            reversePathFind(next.tower,pathSection,false); 
            path.AddRange(pathSection); 
            current = next; 
            next = current.nextNode;
            
        }
        
        List<TowerController> pathSection1 = new List<TowerController>(); 
        pathFindPear(current.tower); 
        int minDist = 999; 
        TowerController minTower = null; 
        foreach (var tower in ends) 
        { 
            if (tower.minDist < minDist) 
            {
                minDist = tower.minDist; 
                minTower = tower;
            }
        }
        reversePathFind(minTower,pathSection1,false); 
        path.AddRange(pathSection1);
        return path;
    }

    public bool pathFind()
    {
        List<TowerController> availableStarts = new List<TowerController>();
        
        foreach (var start in starts)
        {
            if (!start.block)
            {
                availableStarts.Add(start);
            }
        }
        if (availableStarts.Count < numPaths)
        {
            return false;
        }
        List<TowerController> availableCheckpoints = new List<TowerController>();
        foreach (var check in checkpoints)
        {
            if (!check.block)
            {
                availableCheckpoints.Add(check);
            }
        }
        if (availableCheckpoints.Count < numCheckpoints)
        {
            return false;
        }

        Hashtable nodes = new Hashtable();
        foreach (var start in availableStarts)
        {
            pathFind(start);
            if (!nodes.Contains(start))
            {
                nodes.Add(start,new Node());
            }
            Node n = (Node)nodes[start];
            n.tower = start;
            foreach (var check in availableCheckpoints)
            {
                
                if (check.minDist < 999)
                {
                    if (!nodes.Contains(check))
                    {
                        nodes.Add(check,new Node());
                    }
                    Node c = (Node)nodes[check];
                    c.tower = check;
                    n.connecetions.Add(c,check.minDist);
                }
            }

            int minDistToEnd = 999;
            foreach (var end in ends)
            {
                
                if (end.minDist < minDistToEnd)
                {
                    minDistToEnd = end.minDist;
                }
            }

            n.distToEnd = minDistToEnd;
        }
        foreach (var start in availableCheckpoints)
        {
            pathFind(start);
            if (!nodes.Contains(start))
            {
                nodes.Add(start,new Node());
            }
            Node n = (Node)nodes[start];
            n.tower = start;
            foreach (var check in availableCheckpoints)
            {
                
                if (check != start &&check.minDist < 999)
                {
                    if (!nodes.Contains(check))
                    {
                        nodes.Add(check,new Node());
                    }

                    Node c = (Node)nodes[check];
                    c.tower = check;
                    n.connecetions.Add(c,check.minDist);
                }
            }

            int minDistToEnd = 999;
            foreach (var end in ends)
            {
                
                if (end.minDist < minDistToEnd)
                {
                    minDistToEnd = end.minDist;
                }
            }

            n.distToEnd = minDistToEnd;
        }

        for (int i = 0; i < 2; i++)
        {
            foreach (Node node in nodes.Values)
            {
                node.collapse();
            }
        }
        
        List<TowerController> realStarts = new List<TowerController>();
        foreach (var start in availableStarts)
        {
            if (((Node)nodes[start]).assignMinDist(numCheckpoints) < 999)
            {
                for (int i = 0; i <= realStarts.Count; i++)
                {
                    if (i == realStarts.Count)
                    {
                        realStarts.Add(start);
                        break;
                    }

                    if (((Node)nodes[start]).minDist < ((Node)nodes[realStarts[i]]).minDist)
                    {
                        realStarts.Insert(i,start);
                        break;
                    }
                }
                
            }
        }

        if (realStarts.Count < numPaths)
        {
            return false;
        }

        foreach (var onePath in path)
        {
            foreach (var tile in onePath)
            {
                tile.setBaseColor(false);
            }
        }
        path = new List<List<TowerController>>();
        for (int i = 0; i < numPaths; i++)
        {
            List<TowerController> nextPath = new List<TowerController>();
            path.Add(nextPath);
            TowerController start = realStarts[i];
            Node current = (Node)nodes[start];
            current.getMinDist(new List<Node>(), numCheckpoints);
            Node next = current.nextNode;
            while (next != null)
            {
                List<TowerController> pathSection = new List<TowerController>();
                pathFind(current.tower);
                reversePathFind(next.tower,pathSection,true);
                nextPath.AddRange(pathSection);
                current = next;
                next = current.nextNode;
            }
            List<TowerController> pathSection1 = new List<TowerController>();
            pathFind(current.tower);
            int minDist = 999;
            TowerController minTower = null;
            foreach (var tower in ends)
            {
                if (tower.minDist < minDist)
                {
                    minDist = tower.minDist;
                    minTower = tower;
                }
            }
            reversePathFind(minTower,pathSection1,true);
            nextPath.AddRange(pathSection1);
        }
        
        return true;
    }

    public class Node
    {
        public int distToEnd;
        public Hashtable connecetions = new Hashtable();
        public int minDist = -1;
        public Node nextNode;
        public TowerController tower;

        public int assignMinDist(int numCheckpoints)
        {
            return (minDist = getMinDist(new List<Node>(), numCheckpoints));
        }

        public int getMinDist(List<Node> visited, int numCpsLeft)
        {
            if (numCpsLeft == 0)
            {
                nextNode = null;
                
                return distToEnd;
            }

            visited.Add(this);
            int minDist = 999;
            foreach (Node connection in connecetions.Keys)
            {
                if (!visited.Contains(connection))
                {
                    int z = connection.getMinDist(visited, numCpsLeft - 1) + (int)connecetions[connection];
                    if (z < minDist)
                    {
                        minDist = z;
                        nextNode = connection;
                    }
                }
            }

            if (minDist < 999)
            {
                nextNode.getMinDist(visited, numCpsLeft - 1);
            }

            visited.Remove(this);
           
            return minDist;
        }

        public void collapse()
        {
            foreach (Node node in connecetions.Keys)
            {
                foreach (Node node2 in node.connecetions.Keys)
                {
                    if (connecetions.Contains(node2) && (int)connecetions[node] + (int)node.connecetions[node2] <= (int)connecetions[node2])
                    {
                        connecetions.Remove(node2);
                        collapse();
                        return;
                    }

                }
            }
        }
    }


    private void reversePathFind(TowerController t, List<TowerController> path, bool change)
    {
        path.Insert(0,t);
        if (change)
        {
            t.setBaseColor(true);
        }

        if (t.minDist == 0)
        {
            return;
        }

        foreach (var tower in t.nextTo)
        {
            if (t.minDist == tower.minDist && t.tileType > 2 && t.tileType == tower.tileType && !path.Contains(tower))
            {
                foreach (var tower2 in tower.nextTo)
                {
                    if (tower2.minDist + 1 == tower.minDist)
                    {
                        reversePathFind(tower, path,change);
                        return;
                    }
                }
            }

            if (t.minDist == tower.minDist + 1 && !t.block)
            {
                reversePathFind(tower,path,change);
                return;
            }

            if (t.minDist == tower.minDist + 3 && t.block)
            {
                reversePathFind(tower,path,change);
                return;
            }
        }
    }
}
