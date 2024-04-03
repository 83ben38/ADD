using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public void setUp(MapScriptableObject map)
    {
        GameObject cloneObject = new GameObject("Tile");
        GameObject tileObject = new GameObject("Tile",typeof(MeshCollider), typeof(MeshRenderer), typeof(MeshFilter));
        tileObject.transform.parent = cloneObject.transform;
        tileObject.AddComponent<Shape>();
        Shape shape = tileObject.GetComponent<Shape>();
        shape.scriptableObject = map.shape;
        for (int i = 0; i < map.xDimensions; i++)
        {
            for (int j = 0; j < map.yDimensions; j++)
            {
                if (map.map[i + (j * map.xDimensions)] == -4)
                {
                    continue;
                }
                Vector2 pos = shape.getPosition(new Vector2(i, j))*map.scaleAmt * 0.1f;
                Quaternion rot = shape.getRotation(new Vector2(i, j));
                GameObject clone = Instantiate(cloneObject, gameObject.transform, false);
                clone.transform.rotation = rot;
                clone.transform.localPosition = new Vector3(pos.x,0,pos.y) + map.startPos * map.scaleAmt * 0.1f;
                clone.transform.localScale *= map.scaleAmt * 0.1f;
                clone.GetComponentInChildren<Shape>().Reset();
                switch (map.map[i+(j*map.xDimensions)])
                {
                    case 0 : clone.GetComponentInChildren<Renderer>().material.color = ColorManager.manager.tile;
                        break;
                    case 1 : clone.GetComponentInChildren<Renderer>().material.color = ColorManager.manager.wallPerm;
                        break;
                    case 2 : clone.GetComponentInChildren<Renderer>().material.color = ColorManager.manager.tilePerm;
                        break;
                    case 3 : clone.GetComponentInChildren<Renderer>().material.color = ColorManager.manager.portal1;
                        break;
                    case 4 : clone.GetComponentInChildren<Renderer>().material.color = ColorManager.manager.portal2;
                        break;
                    case 5 : clone.GetComponentInChildren<Renderer>().material.color = ColorManager.manager.portal3;
                        break;
                    case -1 : clone.GetComponentInChildren<Renderer>().material.color = ColorManager.manager.start;
                        break;
                    case -2 : clone.GetComponentInChildren<Renderer>().material.color = ColorManager.manager.end;
                        break;
                    case -3 : clone.GetComponentInChildren<Renderer>().material.color = ColorManager.manager.checkpoint;
                        break;
                }
            }
        }
        Destroy(cloneObject);
    }
}
