using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileCode
{
    public int damage;
    public int pierce;
    public int lvl;
    public Transform target;
    public int pierceLeft;

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

    public void tick()
    {
        //do projectile stuff
    }

    public void hit(FruitCode fruit)
    {
        //do projectile hit stuff
    }
}
