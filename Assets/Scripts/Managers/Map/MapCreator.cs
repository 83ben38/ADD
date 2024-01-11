using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.Rendering;

public class MapCreator : MonoBehaviour
{
    public GameObject cloneObject;
    public static Shape shape;
    public int xDimensions;
    public int yDimensions;
    void Start()
    {
        shape = cloneObject.GetComponentInChildren<Shape>();
        CreateMap();
        PathfinderManager.manager.shapeCode = shape.code;
        PathfinderManager.manager.Run();
    }

    void CreateMap()
    {
        for (int i = 0; i < xDimensions; i++)
        {
            for (int j = 0; j < yDimensions; j++)
            {
                Vector2 pos = shape.getPosition(new Vector2(i, j));
                cloneObject.transform.position = new Vector3(pos.x,0,pos.y);
                TowerController t = Instantiate(cloneObject).GetComponent<TowerController>();
                t.name = "Tile " + i + ", " + j;
                PathfinderManager.manager.tiles.Add(t);
                t.x = i;
                t.y = j;
            }
        }
        Destroy(cloneObject);
    }
}
