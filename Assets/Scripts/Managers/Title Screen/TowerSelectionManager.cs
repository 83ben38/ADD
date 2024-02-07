using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerSelectionManager : MonoBehaviour
{
    public GameObject cloneObject;
    public Shape shape;
    public static TowerController[] selected;

    private void Start()
    {
        
        shape = cloneObject.GetComponentInChildren<Shape>();
        MapCreator.shape = shape;
        shape.scriptableObject = SelectionData.data.map.shape;
        CreateMap();
       
    }

    void CreateMap()
    {
        selected = new TowerController[5];
        for (int i = 0; i < 5; i++)
        {
            Vector2 pos = shape.getPosition(new Vector2(i, 8));
            cloneObject.transform.position = new Vector3(pos.x,0,pos.y);
            TowerController t = Instantiate(cloneObject).GetComponent<TowerController>();
            t.state = new SelectionState();
            selected[i] = t;
        }

        int[] towers = SaveData.save.getAvailableTowers();
        for (int j = 0; j < towers.Length; j++)
        {
            Vector2 pos = shape.getPosition(new Vector2(j%5, 8-((j/5)+3)));
            cloneObject.transform.position = new Vector3(pos.x,0,pos.y);
            TowerController t = Instantiate(cloneObject).GetComponent<TowerController>();
            t.state = new SelectionState();
            t.tower = TowerCodeFactory.getTowerCode(towers[j]);
            t.tower.lvl = 1;
            t.x = towers[j];
            Vector3 scale = t.transform.localScale;
            t.transform.localScale = new Vector3(scale.x, scale.y*2, scale.z);
            t.towerVisual.updateTower();
        }
        Destroy(cloneObject);
    }
}