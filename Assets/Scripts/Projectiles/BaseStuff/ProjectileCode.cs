using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public abstract class ProjectileCode
{
    
    public int damage = 1;
    public int pierce = 3;
    public float speed = 2f;
    public int lvl;
    public FruitCode target;
    public int pierceLeft;
    public Vector3 move;
    public List<FruitCode> pierced = new List<FruitCode>();
    public bool upgrade1, upgrade2, upgrade3;
    public virtual void Start(ProjectileController controller)
    {
        controller.transform.localScale *= MapCreator.scale;
        pierceLeft = getPierce();
        speed *= MapCreator.scale;
    }

    public ProjectileCode(bool upgrade1, bool upgrade2, bool upgrade3)
    {
        this.upgrade1 = upgrade1;
        this.upgrade2 = upgrade2;
        this.upgrade3 = upgrade3;
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
            move = lvl * speed * (target.transform.position - controller.transform.position).normalized;
        }
        else if (controller.transform.position.y <= 1)
        {
            move.y = 0;
        }

        controller.transform.Translate(Time.deltaTime*move);
        Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f*MapCreator.scale, LayerMask.GetMask("Enemy"));
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
