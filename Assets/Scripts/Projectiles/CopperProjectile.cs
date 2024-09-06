using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperProjectile : ProjectileCode
{
    public float ticksLeft = 8f;
    public LineRenderer lineRenderer;
    private int damage;
    public bool destroy = false;
    public override int getDamage()
    {
        return lvl * (upgrade3 ? 2 : 1);
    }

    public void doDamage()
    {
        target.Damage(getDamage());
    }

    public override void Start(ProjectileController controller)
    {
        base.Start(controller);
        
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
        
    }


    public override void tick(ProjectileController controller)
    {
        
        if (target == null)
        {
            destroy = true;
        }
        if (destroy)
        {
            ticksLeft -= Time.deltaTime * 64f;
            if (ticksLeft <= 0)
            {
                Object.Destroy(controller.gameObject);
            }

            return;
        }
        if (immediate)
        {
            doDamage();
            target = null;
            Object.Destroy(controller.gameObject);
        }
        lineRenderer.SetPosition(1, target.transform.position);
    }

    public CopperProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        
        immediate = false;
    }
    public CopperProjectile(bool upgrade1, bool upgrade2, bool upgrade3, bool immediate) : base(upgrade1, upgrade2, upgrade3)
    {
        
        this.immediate = immediate;
    }

    private bool immediate;
}

