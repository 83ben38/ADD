using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronProjectile : ProjectileCode
{
    public TowerController targetPath;
    private Vector3 targetPosition;
    
    public override void Start(ProjectileController controller)
    {
        damage = 1;
        base.Start(controller);
        targetPosition = targetPath.transform.position;
        targetPosition.y += 0.5f;
        Vector2 circle = Random.insideUnitCircle * 0.4f;
        targetPosition.x += circle.x;
        targetPosition.z += circle.y;
        
    }

    public override int getPierce()
    {
        return 1;
    }

    public override void tick(ProjectileController controller)
    {
        //do projectile stuff
        if (target != null)
        {
            move = lvl * speed * (target.transform.position - controller.transform.position).normalized;
            controller.transform.Translate(Time.deltaTime * move);
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, .25f*MapCreator.scale, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
            }
        }
        else { 
            if (targetPosition != Vector3.zero)
            {
                move = lvl * speed * (targetPosition - controller.transform.position).normalized;
                controller.transform.Translate(Time.deltaTime * move);
                if ((targetPosition - controller.transform.position).magnitude < 0.05f*lvl)
                {
                    targetPosition = Vector3.zero;
                }
            }
            Collider[] hit = Physics.OverlapSphere(controller.transform.position, .5f*MapCreator.scale, LayerMask.GetMask("Enemy"));
            if (hit.Length > 0)
            {
                target = hit[0].gameObject.GetComponent<FruitCode>();
            }
        }
    }
}
