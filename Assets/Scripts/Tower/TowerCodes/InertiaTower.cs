using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertiaTower : TowerCode
{
    private float accelerationFactor = 64f;
    public InertiaTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 2.5f;
        attackSpeed = 128;
    }

    public override bool shoot()
    {
        if (upgrade1)
        {
            Ray ray = MouseManager.manager.cameraTransform.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Vector3 targetPos = (hit.point - controller.transform.position).normalized;
                targetPos.y = -MapCreator.scale;
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = create();
                pc.code.lvl = lvl > 1 ? lvl : 2;
                pc.code.move =  targetPos * pc.code.lvl;
                projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
                pc.material.color = getColor();
                pc.code.Start(pc);
                return true;
            }

            return false;
        }
        else
        {
            return base.shoot();
        }
    }

    public override void tick()
    {
        if (ticksLeft > 0)
        {
            ticksLeft -= Time.deltaTime*accelerationFactor;
            accelerationFactor += (upgrade3 ? 6 : 8) * Time.deltaTime * lvl;
        }

        if (ticksLeft <= 0)
        {
            
            if (shoot())
            {
                ticksLeft = getAttackSpeed() + ticksLeft;
                rechargeTime = (getAttackSpeed() - 1)*(64f/accelerationFactor);
            }
        }
    }

    public override void MouseClick(TowerController controller)
    {
        
    }

    public override ProjectileCode create()
    {
        return new InertiaProjectile(upgrade1, upgrade2, upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.inertiaTower;
    }

    public override void roundStart()
    {
        base.roundStart();
        accelerationFactor = 64f;
    }

    public override object Clone()
    {
        return new InertiaTower(upgrade1, upgrade2, upgrade3);
    }
}
