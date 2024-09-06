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
        target.Damage(damage);
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
        if (immediate)
        {
            doDamage();
            target = null;
        }
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
        lineRenderer.SetPosition(1, target.transform.position);
    }

    public CopperProjectile(bool upgrade1, bool upgrade2, bool upgrade3, int damage) : base(upgrade1, upgrade2, upgrade3)
    {
        this.damage = damage;
        immediate = false;
    }
    public CopperProjectile(bool upgrade1, bool upgrade2, bool upgrade3, int damage, bool immediate) : base(upgrade1, upgrade2, upgrade3)
    {
        this.damage = damage;
        this.immediate = immediate;
    }

    private bool immediate;
}

