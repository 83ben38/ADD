using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpaceProjectile : ProjectileCode
{
    private float time;
    public Vector3 targetPos;
    public override int getPierce()
    {
        return upgrade1 ? 5 : 1;
    }

    public override int getDamage()
    {
        return 10 * lvl * (upgrade1 ? 3 : 1);
    }


    public override void tick(ProjectileController controller)
    {
        time += Time.deltaTime * lvl;
        if (upgrade1)
        {
            if (time < 1)
            {
                controller.transform.position += new Vector3(0, Time.deltaTime * lvl * 10f, 0);
            }
            else if (time > 2)
            {
                Collider[] hit = Physics.OverlapSphere(targetPos, .5f*MapCreator.scale, LayerMask.GetMask("Enemy"));
                for (int i = 0; i < hit.Length; i++)
                {
                    this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
                }
                Object.Destroy(controller.gameObject);
            }
            else
            {
                Transform transform = controller.transform;
                transform.position = new Vector3(targetPos.x, transform.position.y - (Time.deltaTime * lvl * 10f), targetPos.z);
            }
        }
        else
        {
            if (target == null)
            {
                List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(controller.transform.position, 25f,
                    LayerMask.GetMask("Enemy")));
                while (sphere.Count > 0 && sphere[0].gameObject.GetComponent<FruitCode>().hidden)
                {
                    sphere.RemoveAt(0);
                }

                if (sphere.Count == 0)
                {
                    Object.Destroy(controller.gameObject);
                    return;
                }

                target = sphere[0].gameObject.GetComponent<FruitCode>();
            }

            if (time < 1)
            {
                controller.transform.position += new Vector3(0, Time.deltaTime * lvl * 10f, 0);
            }
            else if (time > 2)
            {
                hit(target, controller);
            }
            else
            {


                Vector3 pos = controller.transform.position;
                Vector3 targetPos = target.transform.position;
                controller.transform.position =
                    new Vector3(targetPos.x, pos.y - (Time.deltaTime * lvl * 10f), targetPos.z);

            }
        }
    }

    public SpaceProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
    }
}
