using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerVisual : MonoBehaviour
{
    [SerializeField]
    public TowerController c;
    [SerializeField]
    private GameObject[] children = new GameObject[7];
    private Shape shape;
    private int shootNum = 0;
    private void Start()
    {
        shape = MapCreator.shape;
        for (int i = 0; i < children.Length; i++)
        {
            children[i].transform.localPosition = shape.getDotPos(i);
        }
        updateTower();
    }

    

    public void updateTower()
    {
        
        foreach (var child in children)
        { 
            child.SetActive(false);
        }
        if (c.tower == null)
        {
            return;
        }
        setColor(c.tower.getColor());
        int z = c.tower.lvl;
        if (z % 2 == 1)
        {
            children[0].SetActive(true);
        }

        if (z > 1)
        {
            children[2].SetActive(true);
            children[5].SetActive(true);
        }
        if (z > 3)
        {
            children[1].SetActive(true);
            children[4].SetActive(true);
        }
        if (z > 5)
        {
            children[3].SetActive(true);
            children[6].SetActive(true);
        }
    }

    public Vector3 shoot()
    {
        int[] nums;
        if (c.tower.lvl % 2 == 1)
        {
            nums = new [] { 0, 2, 5, 1, 4, 3, 6 };
        }
        else
        {
            nums = new [] { 2, 5, 1, 4, 3, 6 };
        }
        
        GameObject d = children[nums[shootNum]];
        shootNum++;
        StartCoroutine(recharge(d));
        return d.transform.position;
    }

    public IEnumerator recharge(GameObject d)
    {
        d.transform.localScale = new Vector3(0,0,0);
        for (int i = 0; i < 25; i++)
        {
            d.transform.localScale = new Vector3(i * .01f, i * .01f, i * .01f);
            yield return null;
        }
    }

    public void setColor(Color co)
    {
        foreach (var child in children)
        {
            child.GetComponent<Renderer>().material.color = co;
        }
    }
}