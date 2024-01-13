using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class IceProjectile : ProjectileCode
{
    public IceProjectile()
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
        float z = .03f * lvl / (float)(Math.Log(fruit.hp, 2) + 1);
        fruit.speed -= z;
        for (float i = 0; i < 16f; i+=Time.deltaTime*64f)
        {
            yield return null;
        }

        fruit.speed += z;
    }
}
