using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperProjectile : ProjectileCode
{
    public float ticksLeft = 4f;
    public LineRenderer lineRenderer;
    private int damage;
    public override int getDamage()
    {
        return lvl;
    }
    

    

    public override void tick(ProjectileController controller)
    {
        if (lineRenderer == null)
        {
            lineRenderer = new GameObject("Copper Electricity").AddComponent<LineRenderer>();
            lineRenderer.material = new Material(controller.material);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, controller.transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
            lineRenderer.useWorldSpace = true;
            lineRenderer.generateLightingData = true;
            lineRenderer.transform.SetParent(controller.transform);
            target.Damage(damage);
        }

        ticksLeft -= Time.deltaTime * 64;
        if (ticksLeft <= 0)
        {
            Object.Destroy(controller.gameObject);
        }
        
        
    }

    public CopperProjectile(bool upgrade1, bool upgrade2, bool upgrade3, int damage) : base(upgrade1, upgrade2, upgrade3)
    {
        this.damage = damage;
    }
}

