using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersManager : MonoBehaviour
{
    public GameObject cloneObject;
    public Shape shape;
    public ShapeScriptableObject SSO;

    private void Start()
    {
        
        shape = cloneObject.GetComponentInChildren<Shape>();
        MapCreator.shape = shape;
        shape.scriptableObject = SSO;
        CreateMap();
       
    }

    void CreateMap()
    {
        int[] towers = SaveData.save.getAvailableTowers();
        for (int j = 0; j < towers.Length; j++)
        {
            Vector2 pos = shape.getPosition(new Vector2(j%5, 10-((j/5)+3)));
            Quaternion rot = shape.getRotation(new Vector2(j%5, 10-((j/5)+3)));
            cloneObject.transform.rotation = rot;
            cloneObject.transform.position = new Vector3(pos.x,0,pos.y);
            TowerController t = Instantiate(cloneObject).GetComponent<TowerController>();
            t.state = new DescriptionState();
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
