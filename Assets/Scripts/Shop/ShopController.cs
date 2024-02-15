using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController manager;
    public ShapeScriptableObject sobject;
    public static Shape shopShape;
    public GameObject towerObject;
    public GameObject[] otherButtons;
    public LootCrateScriptableObject[] availableCrates;
    public GameObject[] crateObjects;
    public GameObject[] buttons;
    public GameObject buttonObject;
    public int screenNum = 0;
    private void Start()
    {
        shopShape = towerObject.GetComponentInChildren<Shape>();
        shopShape.scriptableObject = sobject;
        towerObject.GetComponent<TowerController>().state = new DescriptionState();
        towerObject.transform.localScale *= 0.5f;
        manager = this;
        buttons = new GameObject[availableCrates.Length];
            

        for (int i = 0; i < availableCrates.Length; i++) 
        {
            buttons[i] = Instantiate(buttonObject);
            buttons[i].transform.position = crateObjects[i].transform.position - new Vector3(0, 1, .5f);
            LootButtonController l =buttons[i].GetComponent<LootButtonController>();
            l.crate = new LootCrateScriptableObject(availableCrates[i]);
            l.crateObject = crateObjects[i];
            l.SetUp();
        }
        setEnabled();
    }

    public void setEnabled()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            crateObjects[i].SetActive(screenNum == i);
            buttons[i].SetActive(screenNum == i);
            if (screenNum == i)
            {
                buttons[i].GetComponent<LootButtonController>().SetUp();
            }
        }
    }
}
