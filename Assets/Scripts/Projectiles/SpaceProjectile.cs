using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpaceProjectile : ProjectileCode
{
    private float time;
    public override int getPierce()
    {
        return 1;
    }

    public override int getDamage()
    {
        return 10 * lvl * lvl;
    }
    

    public override void tick(ProjectileController controller)
    {
        time += Time.deltaTime * lvl;
        if (target == null)
        {
            Object.Destroy(controller.gameObject);
            return;
        }
        //do projectile stuff
        if (time < 1)
        {
            controller.transform.position += new Vector3(0,Time.deltaTime * lvl * 10f,0);
        }
        else if (time > 2)
        {
            hit(target, controller);
        }
        else
        {
            

            Vector3 pos = controller.transform.position;
            Vector3 targetPos = target.transform.position;
            controller.transform.position = new Vector3(targetPos.x,pos.y - (Time.deltaTime * lvl * 10f),targetPos.z);
            
        }

    }
}
