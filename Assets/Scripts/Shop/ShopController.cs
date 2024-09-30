using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController manager;
    public ShapeScriptableObject sobject;
    public static Shape shopShape;
    public GameObject loadoutObject;
    public GameObject towerObject;
    public GameObject[] otherButtons;
    public LootCrateScriptableObject[] availableCrates;
    public TextMeshPro crateTitle;
    public GameObject[] crateObjects;
    public GameObject[] buttons;
    public GameObject buttonObject;
    public int screenNum = 0;
    private void Start()
    {
        loadoutObject.transform.localScale *= 0.25f;
        shopShape = towerObject.GetComponentInChildren<Shape>();
        shopShape.scriptableObject = sobject;
        towerObject.GetComponent<TowerController>().state = new EmptyState();
        Vector3 scale = towerObject.transform.localScale;
        towerObject.transform.localScale = new Vector3(scale.x*0.35f,scale.y*0.7f,scale.z*0.35f);
        manager = this;
        buttons = new GameObject[availableCrates.Length];
            

        for (int i = 0; i < availableCrates.Length; i++) 
        {
            buttons[i] = Instantiate(buttonObject);
            buttons[i].transform.position = new Vector3(0,4.5f,-5);
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
                crateTitle.text = availableCrates[i].crateName;
            }
        }
    }
}
