using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WindProjectile : ProjectileCode

{
    private new TowerController target;  


    public WindProjectile(bool upgrade1, bool upgrade2, bool upgrade3, TowerController target) : base(upgrade1, upgrade2, upgrade3)
    {
        this.target = target;
    }

    public override void tick(ProjectileController controller)
    {
        
        move = target.transform.position - controller.transform.position;
        move.y = 0;
        move = move.normalized;
        move *= lvl * speed;
        controller.transform.Translate(Time.deltaTime*move);
        if (move.magnitude < 0.03f * lvl)
        {
            Object.Destroy(controller.gameObject);
        }
    }
}
