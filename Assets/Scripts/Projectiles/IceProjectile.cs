using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class IceProjectile : ProjectileCode
{
    public Vector3 startPos;
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
        if (pierced.Contains(fruit))
        {
            return;
        }
        SoundEffectsManager.manager.playSound("ice");
        pierced.Add(fruit);
        if (fruit.Equals(target))
        {
            target = null;
        }
        fruit.Damage(0);
        float freezeAmount = 0.3f;
        if (upgrade3)
        {
            freezeAmount = 0.3f * (controller.transform.position - startPos).magnitude / (lvl > 4 ? 2 : 1);
        }
        ColorManager.manager.StartCoroutine(hitEnemy(fruit,freezeAmount));
        pierceLeft--;
        if (pierceLeft < 1)
        {
            Object.Destroy(controller.gameObject);
        }
    }
    IEnumerator hitEnemy(FruitCode fruit, float freezeAmount)
    {
        float z = freezeAmount * (upgrade1 ? 1 : lvl) / (float)(Math.Log(fruit.hp, 3) + 1);
        fruit.speed -= z;
        for (float i = 0; i < (lvl > 4 ? 24f : 16f) * (upgrade1 ? lvl : 1); i+=Time.deltaTime*64f)
        {
            yield return null;
        }

        fruit.speed += z;
    }
}
