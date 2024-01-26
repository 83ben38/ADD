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
    private static float aboveAmount = 1f;
    public AtomicProjectile(int num, Vector3 startPos)
    {
        pathNum = num;
        centerPos = startPos;
        centerPos.y += aboveAmount*MapCreator.scale;
        this.startPos = startPos + new Vector3[]
        {
            new Vector3(-1,0,0),
            new Vector3(1,0,0),
            new Vector3(0,0,1),
            new Vector3(0, 0,-1),
            new Vector3(-2,0,0),
            new Vector3(2,0,0)
        }[num]*MapCreator.scale;
        this.startPos.y += aboveAmount;
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
            int offset = new int[]
            {
                2,
                0,
                3,
                1,
                2,
                0
            }[pathNum];
            float degrees = (time - (1f+offset)) * (float)Math.PI / 2.0f;
            double xPosition = Math.Cos(degrees);
            double zPosition = Math.Sin(degrees);
            if (pathNum > 3)
            {
                xPosition *= 2;
                zPosition *= 2;
            }
            
            controller.transform.position = new Vector3((float)xPosition, 0, (float)zPosition)*MapCreator.scale + centerPos;
        }

        Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f*MapCreator.scale, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < pierced.Count; i++)
        {
            for (int j = 0; j <= hit.Length; j++)
            {
                if (j == hit.Length)
                {
                    pierced.RemoveAt(i);
                    i--;
                    break;
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
