using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceTower : TowerCode
{
    private Vector3 targetPos = Vector3.zero;
    public SpaceTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        attackSpeed = 256;
        
    }

    public override int getAttackSpeed()
    {
        switch (lvl)
        {
            case 5 : return 192;
            case 6 : return 144;
            case 7 : return 96;
        }
        return base.getAttackSpeed();
    }

    public override float getRange()
    {
        return -1.5f;
    }

    public override void MouseClick(TowerController controller)
    {
        MouseManager.manager.input.Mouse.LeftClick.performed += setPos;
    }

    public void setPos(InputAction.CallbackContext c)
    {
        MouseManager.manager.input.Mouse.LeftClick.performed -= setPos;
        RaycastHit hit;
        Ray ray = MouseManager.manager.cameraTransform.ScreenPointToRay(UnityEngine.Input.mousePosition);
        Physics.Raycast(ray, out hit,1000);
        targetPos = hit.point;
        targetPos.y = MapCreator.scale;
    }

    public override bool shoot()
    {
        SoundEffectsManager.manager.playSound("artillery-fire");
        if (upgrade1)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl > 1 ? lvl : 2;
            ((SpaceProjectile)pc.code).targetPos = targetPos;
            projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
            pc.material.color = getColor();
            pc.code.Start(pc);
            return true;
        }

        List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(self, 25f,LayerMask.GetMask("Enemy")));
        while (sphere.Count > 0 && sphere[0].gameObject.GetComponent<FruitCode>().hidden)
        {
            sphere.RemoveAt(0);
        }
        if (sphere.Count > 0)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl > 1 ? lvl : 2;
            pc.code.target = sphere[0].GameObject().GetComponent<FruitCode>();
            projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
            pc.material.color = getColor();
            pc.code.Start(pc);
            return true;
        }
        return false;
    }
    

    public override ProjectileCode create()
    {
        return new SpaceProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.spaceTower;
    }

    public override object Clone()
    {
        return new SpaceTower(upgrade1, upgrade2, upgrade3);
    }
}
