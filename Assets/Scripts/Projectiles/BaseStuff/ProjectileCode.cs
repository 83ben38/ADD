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
    public List<FruitCode> pierced = new List<FruitCode>();

    public virtual void Start()
    {
        pierceLeft = getPierce();
    }

    public virtual int getDamage()
    {
        return lvl * damage;
    }
    public virtual int getPierce()
    {
        return pierce;
    }

    public virtual void tick(ProjectileController controller)
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

    public virtual void hit(FruitCode fruit, ProjectileController controller)
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
        if (pierceLeft < 1)
        {
            Object.Destroy(controller.gameObject);
        }
    }
}
