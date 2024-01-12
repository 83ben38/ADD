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

        controller.StartCoroutine(hitEnemy(fruit));
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
            for (int j = 0; j < 32; j++)
            {
                yield return null;
            }
            fruit.Damage(lvl);
            if (fruit == null)
            {
                break;
            }
        }
    }
}
