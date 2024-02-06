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
        return lvl;
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
        ColorManager.manager.StartCoroutine(hitEnemy(fruit));
        pierceLeft--;
        if (pierceLeft < 1)
        {
            Object.Destroy(controller.gameObject);
        }
    }
    IEnumerator hitEnemy(FruitCode fruit)
    {
        float z = .03f * lvl / (float)(Math.Log(fruit.hp, 3) + 1);
        fruit.speed -= z;
        for (float i = 0; i < (lvl > 4 ? 24f : 16f); i+=Time.deltaTime*64f)
        {
            yield return null;
        }

        fruit.speed += z;
    }
}
