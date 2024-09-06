using UnityEngine;
using UnityEngine.InputSystem;

public class LifeTower : TowerCode
{
    private TowerCode tc;

    public LifeTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        tc = this;
        attackSpeed = upgrade1 ? 512 : 256;
    }

    public override void MouseClick(TowerController controller)
    {
        if (upgrade2)
        {
            MouseManager.manager.input.Mouse.LeftClick.performed += Switch;
        }
    }

    private void Switch(InputAction.CallbackContext callbackContext)
    {
        MouseManager.manager.input.Mouse.LeftClick.performed -= Switch;
        if (MouseManager.manager.mouseOn != null && MouseManager.manager.mouseOn is TowerController { tower: not null } t)
        {
            tc = t.tower;
        }
    }

    public override bool shoot()
    {
        SoundEffectsManager.manager.playSound("life");
        GameObject projectile = Object.Instantiate(TowerCode.projectile);
        ProjectileController pc = projectile.GetComponent<ProjectileController>();
        pc.code = new LifeProjectile(upgrade1,upgrade2,upgrade3,tc,lvl > 1 ? lvl : 2);
        Vector2 rand = Random.insideUnitCircle*(MapCreator.scale*getRange());
        Vector3 pos = controller.transform.position;
        ((LifeProjectile)pc.code).target = new Vector3(pos.x+rand.x,pos.y+(1.8f*MapCreator.scale),pos.z+rand.y);
        ((LifeProjectile)pc.code).start = new Vector3(pos.x,pos.y+(1.8f*MapCreator.scale),pos.z);
        projectile.transform.position = controller.towerVisual.shoot(rechargeTime);
        pc.material.color = tc.getColor();
        pc.code.Start(pc);
        return true;
    }

    public override ProjectileCode create()
    {
        return new LifeSmallProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.lifeTower;
    }

    public override object Clone()
    {
        return new LifeTower(upgrade1, upgrade2, upgrade3);
    }
}
