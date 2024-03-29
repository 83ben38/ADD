using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoisonProjectile : ProjectileCode
{
    public PoisonProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3)
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
        fruit.Damage(0);
        controller.StartCoroutine(hitEnemy(fruit));
        pierceLeft--;
        if (pierceLeft < 1)
        {
            Object.Destroy(controller.gameObject);
        }
    }
    IEnumerator hitEnemy(FruitCode fruit)
    {
        fruit.vulnerability++;
        for (float i = 0; i < lvl*3f; i+=Time.deltaTime)
        {
            yield return null;
        }
        fruit.vulnerability--;
    }
}
