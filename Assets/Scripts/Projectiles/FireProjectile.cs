using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class FireProjectile : ProjectileCode
{ 
    public override int getPierce()
    {
        return lvl * 2;
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
        for (int i = 0; i < lvl; i++)
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
}
