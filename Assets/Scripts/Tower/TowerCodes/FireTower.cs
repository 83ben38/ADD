using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireTower : TowerCode
{
    public bool abilityUsed = false;
    public FireTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 2;
        if (upgrade2)
        {
            range = 1;
            attackSpeed /= 2;
        }
        attackSpeed = (int)(attackSpeed * (upgrade1 ? 1.15 : 1) * (upgrade3 ? 1.25 : 1));
    }

    public override void MouseClick(TowerController controller)
    {
        if (upgrade3 && !abilityUsed)
        {
            abilityUsed = true;
            MouseManager.manager.input.Mouse.LeftClick.performed += shoot;
        }
    }

    public void shoot(InputAction.CallbackContext c)
    {
        MouseManager.manager.input.Mouse.LeftClick.performed -= shoot;
        RaycastHit hit;
        Ray ray = MouseManager.manager.cameraTransform.ScreenPointToRay(UnityEngine.Input.mousePosition);
        Physics.Raycast(ray, out hit,1000);
        Vector3 v3 = hit.transform.position;
        v3.y = MapCreator.scale;
        controller.StartCoroutine(shootAsync(v3));
    }

    public IEnumerator shootAsync(Vector3 target)
    {
        for (int i = 0; i < 10 * (upgrade2 ? 2 : 1); i++)
        {
            for (float j = 0; j < (upgrade2 ? .1f : .2f); j+=Time.deltaTime)
            {
                yield return null;
            }
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl > 1 ? lvl : 2;
            projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
            pc.code.move = target - projectile.transform.position;
            pc.code.move.y =0;
            pc.code.pierceLeft *= 4;
            pc.code.move = pc.code.move.normalized *(pc.code.lvl * pc.code.speed);
            pc.code.move.y = -1*(pc.code.lvl * pc.code.speed);;
            pc.material.color = getColor();
            pc.code.Start(pc);
        }
    }

    public override void roundStart()
    {
        abilityUsed = false;
        base.roundStart();
    }

    public override ProjectileCode create()
    {
        return new FireProjectile(upgrade1,upgrade2,upgrade3);
    }
    

    public override Color getColor()
    {
        return ColorManager.manager.fireTower;
    }

    public override object Clone()
    {
        return new FireTower(upgrade1, upgrade2, upgrade3);
    }
}
