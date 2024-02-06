using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaterProjectile : ProjectileCode
{
    private bool hitEnemy = false;
    private float explosionAmount = 1f;
    private Vector3 baseTransform;
    public WaterProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3)
    {
        damage = 2;
    }
    public override void tick(ProjectileController controller)
    {
        //do projectile stuff
        if (!hitEnemy)
        {
            if (target != null)
            {
                move = lvl * speed * (target.transform.position - controller.transform.position).normalized;
            }
            else
            {
                move.y = 0;
            }
            controller.transform.Translate(Time.deltaTime * move);
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
            }

            if (hit.Length > 0)
            {
                baseTransform = controller.transform.localScale;
            }
        }
        else
        {
            explosionAmount += Time.deltaTime * lvl * 2;
            controller.transform.localScale = baseTransform * explosionAmount;
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
            }

            if (explosionAmount >= (lvl + 1))
            {
                Object.Destroy(controller.gameObject);
            }
        }
    }

    public override void hit(FruitCode fruit, ProjectileController controller)
    {
        if (pierced.Contains(fruit))
        {
            return;
        }
        hitEnemy  = true;
        pierced.Add(fruit);
        fruit.Damage(getDamage());
    }
}
