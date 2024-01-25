using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;

public class AtomicProjectile : ProjectileCode
{
    private int pathNum;
    private Vector3 startPos;
    private Vector3 centerPos;
    private float time = 0f;
    private static float aboveAmount = 0.5f;
    public AtomicProjectile(int num, Vector3 startPos)
    {
        pathNum = num;
        centerPos = startPos;
        centerPos.y += aboveAmount;
        this.startPos = startPos + new Vector3[]
        {
            new Vector3(-1,aboveAmount,0),
            new Vector3(1,aboveAmount,0),
            new Vector3(0,aboveAmount,1),
            new Vector3(0, aboveAmount,-1),
            new Vector3(-2,aboveAmount,0),
            new Vector3(2,aboveAmount,0)
        }[num];
    }

    public override void tick(ProjectileController controller)
    {
        time += Time.deltaTime * lvl;
        if (time < 1f)
        {
            Vector3 difference = startPos - centerPos;
            difference *= time;
            difference += centerPos;
            controller.transform.position = difference;
        }
        else
        {
            float degrees = (time - 1f) * (float)Math.PI / 2.0f;
            double xPosition = Math.Cos(degrees);
            double zPosition = Math.Sin(degrees);
        }

        Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < pierced.Count; i++)
        {
            for (int j = 0; j <= hit.Length; j++)
            {
                if (j == hit.Length)
                {
                    pierced.RemoveAt(i);
                    i--;
                }

                if (hit[j].gameObject.GetComponent<FruitCode>()==pierced[i])
                {
                    break;
                }
            }
        }
        for (int i = 0; i < hit.Length; i++)
        {
            this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
        }
    }

    public override void hit(FruitCode fruit, ProjectileController controller)
    {
        if (pierced.Contains(fruit))
        {
            return;
        }
        pierced.Add(fruit);
        fruit.Damage(getDamage());
    }
}
