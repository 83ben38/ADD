using System;
using System.Collections;
using System.IO.IsolatedStorage;
using UnityEngine;
using Object = UnityEngine.Object;

public class EarthTower : TowerCode
{
    private static Vector3[] directions6 = new[] {new Vector3(-1,0,0),new Vector3(1,0,0),new Vector3(0.5f,0,0.76f).normalized,new Vector3(-0.5f,0,0.76f).normalized,new Vector3(0.5f,0,-0.76f).normalized,new Vector3(-0.5f,0,-0.76f).normalized };
    private bool abilityUsed = true;
    
    private static Vector3[] directions4 = new[] { new Vector3(1, 0, 0),  new Vector3(0, 0, 1),new Vector3(-1, 0, 0), new Vector3(0, 0, -1) };
    public EarthTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        
    }

    public override int getAttackSpeed()
    {
        switch (lvl)
        {
           case 5: return 144;
           case 6: return 112;
           case 7: return 80;
        }

        return 192;
    }

    public override float getRange()
    {
        return -1.5f;
    }

    public override bool shoot()
    {
        SoundEffectsManager.manager.playSound("earth");
        controller.towerVisual.shoot(rechargeTime);
        Vector3[] directions = upgrade1 ? directions4 : directions6;
        foreach (var vector3 in directions)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = create();
            pc.code.lvl = lvl > 1 ? lvl : 2;
            pc.code.move = MapCreator.scale*2*vector3;
            projectile.transform.position = controller.transform.position+new Vector3(0,1f*MapCreator.scale,0);
            pc.material.color = getColor();
            pc.code.Start(pc);
        }
        return true;
    }

    public override void MouseClick(TowerController controller)
    {
        if (!abilityUsed)
        {
            abilityUsed = true;
            this.controller.StartCoroutine(earthquake());
        }
    }

    public IEnumerator earthquake()
    {
        float radians = 0;
        Vector3[] directions = upgrade1 ? directions4 : directions6;
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < directions.Length; j+=2)
            {
                Vector3 vector3 = new Vector3((float)(directions[j].x*Math.Cos(radians) + directions[j].z*Math.Sin(radians)),0,(float)(directions[j].z*Math.Cos(radians) + directions[j].x*Math.Sin(radians))).normalized;
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = create();
                pc.code.lvl = lvl > 1 ? lvl : 2;
                pc.code.move = MapCreator.scale*2*vector3;
                projectile.transform.position = controller.transform.position+new Vector3(0,1f*MapCreator.scale,0);
                pc.material.color = getColor();
                pc.code.Start(pc);
            }
            for (float j = 0; j < 0.125f; j+=Time.deltaTime)
            {
                yield return null;
            }

            radians += upgrade1 ? (float)Math.PI / 6 : (float)Math.PI / 9;
        }
    }

    public override void roundStart()
    {
        base.roundStart();
        if (upgrade3)
        {
            abilityUsed = false;
        }
    }


    public override ProjectileCode create()
    {
        return new EarthProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.earthTower;
    }

    public override object Clone()
    {
        return new EarthTower(upgrade1, upgrade2, upgrade3);
    }
}
