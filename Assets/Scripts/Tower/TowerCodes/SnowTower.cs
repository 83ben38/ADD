using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class SnowTower : TowerCode
{
    private bool snowStormUsed = false;
    public SnowTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 0.5f;
        attackSpeed = 128;
        if (upgrade3)
        {
            attackSpeed *= 3;
            attackSpeed /= 2;
        }

        snowStormUsed = !upgrade3;
    }

    public override float getRange()
    {
        return (range*2) + (lvl > 4 ? 1.0f : 0.0f);
    }
    
    public override void tick()
    {
        if (ticksLeft > 0)
        {
            ticksLeft -= lvl*Time.deltaTime*32f;
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

    public override void roundStart()
    {
        base.roundStart();
        if (upgrade3)
        {
            snowStormUsed = false;
        }
    }

    public override void MouseClick(TowerController controller)
    {
        if (!snowStormUsed)
        {
            List<TowerController> tiles = PathfinderManager.manager.tiles;
            float startX = tiles[0].transform.position.x;
            float endX = tiles[^1].transform.position.x;
            float startY = tiles[0].transform.position.z;
            float endY = tiles[^1].transform.position.z;
            bool side = true;
            for (float i = startX; i < endX; i += (upgrade2 ? 0.25f : 0.5f))
            {
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = new SnowstormProjectile(upgrade1, upgrade2, upgrade3);
                pc.code.lvl = lvl;
                pc.code.move = new Vector3(0, 0, side ? -3 : 3);
                projectile.transform.position = new Vector3(i, MapCreator.scale, side ? endY + 1 : startY - 1);
                pc.material.color = getColor();
                pc.code.Start(pc);
                side = !side;
            }

            side = true;
            for (float i = startY; i < endY; i += (upgrade2 ? 0.25f : 0.5f))
            {
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = new SnowstormProjectile(upgrade1, upgrade2, upgrade3);
                pc.code.lvl = lvl;
                pc.code.move = new Vector3(side ? -3 : 3, 0, 0);
                projectile.transform.position = new Vector3(side ? endX + 1 : startX - 1, MapCreator.scale, i);
                pc.material.color = getColor();
                pc.code.Start(pc);
                side = !side;
            }
            snowStormUsed = true;
        }
    }

    public override ProjectileCode create()
    {
        return new SnowProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.snowTower;
    }

    public override object Clone()
    {
        return new SnowTower(upgrade1, upgrade2, upgrade3);
    }
}
