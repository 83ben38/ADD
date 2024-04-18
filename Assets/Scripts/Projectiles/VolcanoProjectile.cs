using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoProjectile : ProjectileCode
{
    public bool hasHit;
    private float explosionAmount = 1f;
    private Vector3 baseTransform;
    public VolcanoProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        damage = 5;
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

    public override void tick(ProjectileController controller)
    {
        if (!hasHit)
        {
            if (target != null)
            {
                move = lvl * (target.transform.position - controller.transform.position);
                target = null;
                move.y = 1;
            }
            else
            {
                move.y -= Time.deltaTime*2.5f*lvl;
            }
            controller.transform.Translate(Time.deltaTime * move);
            if (move.y < -1.5f)
            {
                hasHit = true;
            }
        }
        else
        {
            explosionAmount += Time.deltaTime;
            controller.transform.localScale = baseTransform * explosionAmount;
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f*explosionAmount*MapCreator.scale, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
            }

            if (explosionAmount >= 1.5f)
            {
                Object.Destroy(controller.gameObject);
            }
        }
    }

    IEnumerator hitEnemy(FruitCode fruit)
    {
        fruit.Damage(getDamage());
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
}
