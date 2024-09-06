using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightningTower : TowerCode
{
    private bool abilityUsed = true;
    private LineRenderer lineRenderer;
    public LightningTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 3;
        attackSpeed = 192;
        if (upgrade2)
        {
            attackSpeed *= 5;
            attackSpeed /= 4;
        }

        if (upgrade3)
        {
            range = 2;
        }
    }

    public override void tick()
    {
        if (ticksLeft > 0)
        {
            if (upgrade3)
            {
                ticksLeft -= (lvl > 1 ? 2 : 1) * Time.deltaTime * 64f;
            }
            else
            {
                ticksLeft -= lvl * Time.deltaTime * 64f;
            }
        }

        if (ticksLeft <= 0)
        {
            
            if (shoot())
            {
                ticksLeft = getAttackSpeed() + ticksLeft;
                rechargeTime = getAttackSpeed() - 1;
            }
        }
    }

    public override void MouseClick(TowerController controller)
    {
        if (!abilityUsed)
        {
            abilityUsed = true;
            MouseManager.manager.input.Mouse.LeftClick.performed += strike;
        }
    }

    private void strike(InputAction.CallbackContext obj)
    {
        MouseManager.manager.input.Mouse.LeftClick.performed -= strike;
        RaycastHit hit;
        Ray ray = MouseManager.manager.cameraTransform.ScreenPointToRay(UnityEngine.Input.mousePosition);
        Physics.Raycast(ray, out hit,1000);
        Vector3 v3 = hit.transform.position;
        v3.y = MapCreator.scale;
        Collider[] hits = Physics.OverlapSphere(v3, MapCreator.scale, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].gameObject.GetComponent<FruitCode>().Damage(25*lvl*lvl);
        }
        v3.y = 0;
        lineRenderer = new GameObject("Lightning").AddComponent<LineRenderer>();
        lineRenderer.material.color = getColor();
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;
        lineRenderer.positionCount = 3;
        lineRenderer.SetPosition(0,v3);
        v3.y += MapCreator.scale * 5f;
        lineRenderer.SetPosition(2,v3);
        v3.y -= MapCreator.scale * 2.5f;
        v3 += Random.insideUnitSphere*2;
        lineRenderer.SetPosition(1,v3);
        lineRenderer.useWorldSpace = true;
        lineRenderer.generateLightingData = true;
        lineRenderer.transform.SetParent(controller.transform);
        controller.StartCoroutine(delete());
        
    }

    private IEnumerator delete()
    {
        for (float i = 0; i < .5f; i+=Time.deltaTime)
        {
            yield return null;
        }
        
        Object.Destroy(lineRenderer);
    }

    

    public override bool shoot()
    {
        List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(self, getRange() * MapCreator.scale,LayerMask.GetMask("Enemy")));
        if (sphere.Count > 0)
        {
            for (int i = 0; (i < (lvl > 2 ? lvl : 2) && upgrade1) || i < 1; i++)
            {
                if (sphere.Count == i)
                {
                    break;
                }
                FruitCode fc = sphere[i].gameObject.GetComponent<FruitCode>();
                if (fc.hidden)
                {
                    sphere.RemoveAt(0);
                    i--;
                    continue;
                }
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = create();
                pc.code.lvl = lvl > 1 ? lvl : 2;
                pc.code.target = fc;
                projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
                pc.material.color = getColor();
                pc.code.Start(pc);
            }
            return true;
        }
        return false;
    }

    public override void roundStart()
    {
        base.roundStart();
        if (upgrade2)
        {
            abilityUsed = false;
        }
    }

    public override ProjectileCode create()
    {
        return new LightningProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.lightningTower;
    }

    public override object Clone()
    {
        return new LightningTower(upgrade1, upgrade2, upgrade3);
    }
}
