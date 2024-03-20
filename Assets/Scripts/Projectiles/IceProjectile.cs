using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class IceProjectile : ProjectileCode
{
    public IceProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3)
    {
        damage = 0;
    }

    public override int getPierce()
    {
        return lvl * (upgrade1 ? 2 : 1);
    }

    public override void hit(FruitCode fruit, ProjectileController controller)
    {
        SoundEffectsManager.manager.playSound("ice");
        if (pierced.Contains(fruit))
        {
            return;
        }
        pierced.Add(fruit);
        if (fruit.Equals(target))
        {
            target = null;
        }
        fruit.Damage(0);
        ColorManager.manager.StartCoroutine(hitEnemy(fruit));
        pierceLeft--;
        if (pierceLeft < 1)
        {
            Object.Destroy(controller.gameObject);
        }
    }
    IEnumerator hitEnemy(FruitCode fruit)
    {
        float z = .03f * (upgrade1 ? 1 : lvl) / (float)(Math.Log(fruit.hp, 3) + 1);
        fruit.speed -= z;
        for (float i = 0; i < (lvl > 4 ? 24f : 16f) * (upgrade1 ? lvl : 1); i+=Time.deltaTime*64f)
        {
            yield return null;
        }

        fruit.speed += z;
    }
}
