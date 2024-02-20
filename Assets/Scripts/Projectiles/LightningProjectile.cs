using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class LightningProjectile : ProjectileCode
{
    public float ticksLeft = 32f;
    public LineRenderer lineRenderer;
    public override int getDamage()
    {
        return (int)Math.Pow(1.5, pierceLeft);
    }
    

    public override int getPierce()
    {
        return (upgrade1 ? 1 : 2) + lvl;
    }

    public override void tick(ProjectileController controller)
    {
        if (pierceLeft == getPierce())
        {
            if (target != null)
            {
                move = lvl * speed * (target.transform.position - controller.transform.position).normalized;
            }
            else
            {
                move.y = 0;
            }
            controller.transform.Translate(Time.deltaTime*move);
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length && i < 1; i++)
            {
                this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
            }
        }
        else if (pierceLeft == 1)
        {
            ticksLeft -= Time.deltaTime * 64;
            if (ticksLeft <= 0)
            {
                Object.Destroy(controller.gameObject);
            }
        }
        else
        {
            if (lineRenderer == null)
            {
                lineRenderer = new GameObject("Lightning").AddComponent<LineRenderer>();
                lineRenderer.material = new Material(controller.material);
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
                lineRenderer.positionCount = getPierce();
                lineRenderer.SetPosition(0,controller.transform.position);
                lineRenderer.useWorldSpace = true;
                lineRenderer.generateLightingData = true;
                lineRenderer.transform.SetParent(controller.transform);
            }
            Collider[] hit = Physics.OverlapSphere(lineRenderer.GetPosition(getPierce()-pierceLeft-1), 10f, LayerMask.GetMask("Enemy"));
            

            int i = 0;
            while(hit.Length == i || pierced.Contains(hit[i].gameObject.GetComponent<FruitCode>()))
            {
                if (hit.Length == i)
                {
                    pierceLeft = 1;
                    return;
                }
                i++;
            }
            this.hit(hit[i].gameObject.GetComponent<FruitCode>(),controller);
            lineRenderer.SetPosition(getPierce()-pierceLeft,hit[i].gameObject.transform.position);
            
        }
        
    }

    public LightningProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
    }
}

