using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using Object = UnityEngine.Object;

public class EarthProjectile : ProjectileCode
{
    public EarthProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3)
    {
        damage = 2;
        pierce = 10;
    }

    public override void Start(ProjectileController controller)
    {
        base.Start(controller);
        controller.transform.localScale *= 2;
    }
    public override void tick(ProjectileController controller)
    {
        //do projectile stuff
        if (target != null)
        {
            move = lvl * speed * (target.transform.position - controller.transform.position).normalized;
        }
        else
        {
            move.y = 0;
        }

        controller.transform.Translate(Time.deltaTime*move);
        Collider[] hit = Physics.OverlapSphere(controller.transform.position, .5f*MapCreator.scale, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < hit.Length; i++)
        {
            this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
        }
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

        fruit.Damage(getDamage());
        pierceLeft--;
        if (upgrade2)
        {
            controller.StartCoroutine(pushBack(fruit));
        }

        if (pierceLeft < 1)
        {
            Object.Destroy(controller.gameObject);
        }
    }

    public IEnumerator pushBack(FruitCode fruit)
    {
        for (float i = 0; i < 0.125f; i+=Time.deltaTime)
        {
            fruit.transform.position += move * ((Time.deltaTime) / (float)Math.Log(fruit.maxHp,8));
            yield return null;
        }
    }
}
