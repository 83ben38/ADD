using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;

public class ReactorProjectile : ProjectileCode
{
    private int pathNum;
    private Vector3 startPos;
    private Vector3 centerPos;
    private float time = 0f;
    private static float aboveAmount = 1f;
    private bool rotating = false;
    public bool clockwise = true;
    public ReactorProjectile(int num, Vector3 startPos,bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3)
    {
        pathNum = num;
        centerPos = startPos;
        centerPos.y += aboveAmount*MapCreator.scale;
        this.startPos = startPos + new Vector3[]
        {
            new Vector3(-2,0,0),
            new Vector3(2,0,0),
            new Vector3(0,0,2),
            new Vector3(0, 0,-2)
        }[num]*MapCreator.scale;
        this.startPos.y += aboveAmount;
    }

    public override void tick(ProjectileController controller)
    {
        
        
        if (!rotating)
        {
            time += Time.deltaTime * lvl;
            Vector3 difference = startPos - centerPos;
            difference *= time;
            difference += centerPos;
            controller.transform.position = difference;
            if (time > 1f)
            {
                rotating = true;
            }
        }
        else
        {
            time += Time.deltaTime * lvl * (clockwise ? 1 : -1) * 4;
            int offset = new int[]
            {
                2,
                0,
                3,
                1
            }[pathNum];
            float degrees = (time - (1f+offset)) * (float)Math.PI / 2.0f;
            double xPosition = Math.Cos(degrees);
            double zPosition = Math.Sin(degrees);
            xPosition *= 2;
            zPosition *= 2;
            controller.transform.position = new Vector3((float)xPosition, 0, (float)zPosition)*MapCreator.scale + centerPos;
        }

        Collider[] hit = Physics.OverlapSphere(controller.transform.position, .5f*MapCreator.scale, LayerMask.GetMask("Enemy"));
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

    public override int getDamage()
    {
        return 5;
    }
}
