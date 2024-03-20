using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class FireProjectile : ProjectileCode
{ 
    public override int getPierce()
    {
        return 3;
    }

    public override void hit(FruitCode fruit, ProjectileController controller)
    {
        if (pierced.Contains(fruit))
        {
            return;
        }
        pierced.Add(fruit);
        if (fruit.Equals(target))
        {
            target = null;
        }

        if (upgrade1 && fruit.frozenTime > 0)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.code = new WaterProjectile(false,false,false);
            pc.code.lvl = lvl;
            ((WaterProjectile)pc.code).hitEnemy = true;
            projectile.transform.position = controller.transform.position;
            pc.material.color = ColorManager.manager.waterTower;
            pc.code.Start(pc);
        }

        ColorManager.manager.StartCoroutine(hitEnemy(fruit));
        pierceLeft--;
        if (pierceLeft < 1)
        {
            Object.Destroy(controller.gameObject);
        }
    }

    public override int getDamage()
    {
        return lvl;
    }

    IEnumerator hitEnemy(FruitCode fruit)
    {
        SoundEffectsManager.manager.playSound("fire");
        for (int i = 0; i < 3; i++)
        {
            for (float j = 0; j < 0.5f; j+=Time.deltaTime)
            {
                yield return null;
            }
            if (fruit == null)
            {
                break;
            }
            fruit.Damage(getDamage());
        }
    }

    public FireProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
    }
}
