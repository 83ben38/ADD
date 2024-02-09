using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.Rendering;

public class MapCreator : MonoBehaviour
{
    public static float scale;
    public MapScriptableObject map;
    public GameObject cloneObject;
    public static Shape shape;
    private int xDimensions;
    private int yDimensions;
    private Vector3 startPos;
    private int[] types;
    public TowerGhostController controller;
    
    private void Start()
    {
        map = SelectionData.data.map;
        controller.shape.scriptableObject = map.shape;
        Instantiate(controller.gameObject);
        Destroy(controller.gameObject);
        
        shape = cloneObject.GetComponentInChildren<Shape>();
        shape.scriptableObject = map.shape;
        xDimensions = map.xDimensions;
        yDimensions = map.yDimensions;
        startPos = map.startPos;
        scale = map.scaleAmt;
        types = map.map;
        CreateMap();
        PathfinderManager.manager.shapeCode = shape.scriptableObject.checkName;
        PathfinderManager.manager.numPaths = map.paths;
        PathfinderManager.manager.numCheckpoints = map.checkpointsRequired;
        PathfinderManager.manager.Run();
        
    }

    void CreateMap()
    {
        cloneObject.transform.localScale *= scale;
        for (int i = 0; i < xDimensions; i++)
        {
            for (int j = 0; j < yDimensions; j++)
            {
                Vector2 pos = shape.getPosition(new Vector2(i, j))*scale;
                cloneObject.transform.position = new Vector3(pos.x,0,pos.y) + startPos;
                TowerController t = Instantiate(cloneObject).GetComponent<TowerController>();
                t.name = "Tile " + i + ", " + j;
                PathfinderManager.manager.tiles.Add(t);
                t.x = i;
                t.y = j;
                t.setTileType(types[i+(j*xDimensions)]);
            }
        }
        Destroy(cloneObject);
    }
}
