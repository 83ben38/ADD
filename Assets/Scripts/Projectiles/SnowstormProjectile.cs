using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using Math = System.Math;

public class SnowstormProjectile : ProjectileCode
{
    public float radius;
    public SnowstormProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        damage = 4;
        pierce = 3;
        radius = .5f + (lvl * 0.25f);
        
        if (upgrade1)
        {
            damage *= 2;
        }

        if (upgrade2)
        {
            radius /= 2;
            damage /= 2;
        }
    }
   
    public override void tick(ProjectileController controller)
    {
        controller.transform.localScale = new Vector3(radius, radius, radius);
        controller.transform.Translate(Time.deltaTime*move);
        Collider[] hit = Physics.OverlapSphere(controller.transform.position, radius*MapCreator.scale, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < hit.Length; i++)
        {
            this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
        }
    }
    public override int getDamage()
    {
        return base.getDamage()*2;
    }
    public virtual void hit(FruitCode fruit, ProjectileController controller)
    {
        if (pierced.Contains(fruit))
        {
            return;
        }
        pierced.Add(fruit);
        ColorManager.manager.StartCoroutine(hitEnemy(fruit));
        if (fruit.Equals(target))
        {
            target = null;
        }

        fruit.Damage(getDamage());
        fruit.Damage(0);
        fruit.Damage(0);
    }

    public override int getPierce()
    {
        return base.getPierce()*2;
    }
    IEnumerator hitEnemy(FruitCode fruit)
    {
        float z = fruit.speed/2;
        fruit.speed -= z;
        for (float i = 0; i < (upgrade1 ? 320f : 640f); i+=Time.deltaTime*64f)
        {
            yield return null;
        }

        fruit.speed += z;
    }
}
