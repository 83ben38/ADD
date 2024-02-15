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
        shape = MapCreator.shape ?? ShopController.shopShape;
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

    public Vector3 shoot(float rechargeTime)
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
        if (shootNum >= c.tower.lvl)
        {
            shootNum = 0;
        }

        StartCoroutine(recharge(d,rechargeTime));
        return d.transform.position;
    }

    public IEnumerator recharge(GameObject d, float rechargeTime)
    {
        for (float i = 1; i <= rechargeTime; i+=Time.deltaTime*64f)
        {
            d.transform.localScale = new Vector3(i * .25f /  rechargeTime, i * .125f / rechargeTime, i * .25f / rechargeTime);
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
