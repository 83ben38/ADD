using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class LootButtonController : Selectable
{
    public TextMeshPro text;
    public LootCrateScriptableObject crate;
    private Material _material;
    public GameObject crateObject;
    private bool running = false;
    public void SetUp()
    {
        text.text = crate.cost + " \u20b5\u00a2";
        if (crate.cost >= 10000)
        {
            text.text = (crate.cost/1000) + ((crate.cost%1000)/100) + " \u20b5\u00a2";
        }
        _material = GetComponent<Renderer>().material;
        MouseExit();
    }

    public override void MouseEnter()
    {
        _material.color = SaveData.save.getMoney() >= crate.cost ? ColorManager.manager.tileHighlighted :  ColorManager.manager.pathHighlighted;
    }

    public override void MouseExit()
    {
        _material.color = SaveData.save.getMoney() >= crate.cost ? ColorManager.manager.tile :  ColorManager.manager.path;
    }

    public override void MouseClick()
    {
        if (SaveData.save.getMoney() >= crate.cost && !running)
        {
            StartCoroutine(Animate());
        }
    }

    public IEnumerator Animate()
    {
        running = true;
        GameObject[] otherButtons = ShopController.manager.otherButtons;
        for (int i = 0; i < crate.chances.Count; i++)
        {
            for (int j = 0; j < crate.chances[i].items.Count; j++)
            {
                if (crate.loadouts)
                {
                    if (SaveData.save.getAvailableLoadouts().Contains(crate.chances[i].items[j]))
                    {
                        crate.chances[i].items.RemoveAt(j);
                        j--;
                    }
                }
                else
                {
                    if (SaveData.save.isUpgradeAvailable(crate.chances[i].items[j],3))
                    {
                        crate.chances[i].items.RemoveAt(j);
                        j--;
                    }
                }
            }

            if (crate.chances[i].items.Count == 0)
            {
                if (i == 0)
                {
                    yield break;
                }
                crate.chances[0].chance += crate.chances[i].chance; 
                crate.chances.RemoveAt(i);
                i--;
            }
        }
        float f = Random.value;
        float total = 1;
        int item = -1;
        for (int i = 0; i < crate.chances.Count; i++)
        {
            total -= crate.chances[i].chance;
            if (total <= 0)
            {
                item = crate.chances[i].items[(int)(Random.value * crate.chances[i].items.Count)];
                break;
            }
        }

        if (crate.loadouts)
        {
            SaveData.save.setAvailableLoadout(item);
        }
        else
        {
            if (!SaveData.save.getAvailableTowers().Contains(item))
            {
                SaveData.save.setAvailableTower(item);
            }

            for (int i = 0; i < 3; i++)
            {
                if (!SaveData.save.isUpgradeAvailable(item, i))
                {
                    SaveData.save.setUpgradeAvailable(item,i,true);
                    break;
                }
            }
        }

        for (int i = 0; i < otherButtons.Length; i++)
        {
            otherButtons[i].SetActive(false);
        }
        Vector3 pos = crateObject.transform.position;
        Quaternion rot = crateObject.transform.rotation;
        for (float i = 0; i < 0.5f; i+=Time.deltaTime)
        {
            crateObject.transform.Rotate(new Vector3(0,0,Time.deltaTime*60f));
            yield return null;
        }    
        for (float i = 0; i < 0.5f; i+=Time.deltaTime)
        {
            yield return null;
        } 
        for (float i = 0; i < 1; i+=Time.deltaTime)
        {
            crateObject.transform.Rotate(new Vector3(0,0,Time.deltaTime*-60f));
            yield return null;
        } 
        for (float i = 0; i < 0.5f; i+=Time.deltaTime)
        {
            yield return null;
        } 
        for (float i = 0; i < 0.5f; i+=Time.deltaTime)
        {
            crateObject.transform.Rotate(new Vector3(0,0,Time.deltaTime*60f));
            yield return null;
        } 
        for (float i = 0; i < 0.5f; i+=Time.deltaTime)
        {
            yield return null;
        }
        for (float i = 0; i < 1f; i+=Time.deltaTime)
        {
            crateObject.transform.Translate(0,3f*Time.deltaTime,0);
            yield return null;
        }
        for (float i = 0; i < 1f; i+=Time.deltaTime)
        {
            yield return null;
        }
        for (int i = 0; i < otherButtons.Length; i++)
        {
            otherButtons[i].SetActive(true);
        }
        crateObject.transform.SetPositionAndRotation(pos,rot);
        SaveData.save.addMoney(-crate.cost);
        SetUp();
        running = false;
    }
}
