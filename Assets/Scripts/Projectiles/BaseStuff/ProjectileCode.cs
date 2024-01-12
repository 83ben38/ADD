using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public abstract class ProjectileCode
{
    public int damage = 1;
    public int pierce = 3;
    public float speed = .05f;
    public int lvl;
    public FruitCode target;
    public int pierceLeft;
    public Vector3 move;
    private List<FruitCode> pierced = new List<FruitCode>();

    public ProjectileCode()
    {
        pierceLeft = getPierce();
    }

    public int getDamage()
    {
        return lvl * damage;
    }
    public int getPierce()
    {
        return pierce;
    }

    public void tick(ProjectileController controller)
    {
        //do projectile stuff
        if (target != null)
        {
            move = speed * (target.transform.position - controller.transform.position).normalized;
        }
        controller.transform.Translate(move);
        Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < hit.Length; i++)
        {
            this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
        }
    }

    public void hit(FruitCode fruit, ProjectileController controller)
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

        fruit.Damage(damage);
        pierceLeft--;
        if (pierceLeft < 1)
        {
            Object.Destroy(controller.gameObject);
        }
    }
}
