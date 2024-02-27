using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeProjectile : ProjectileCode
{
    public new Vector3 target;
    public Vector3 start;
    private float time = -64f;
    private float totalTime;

    public LifeProjectile(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
    }

    public override void tick(ProjectileController controller)
    {
        if (time < 0f)
        {
            Vector3 difference = target - start;
            difference *= time/64f;
            difference += target;
            controller.transform.position = difference;
        }
        totalTime += lvl*Time.deltaTime*64f;
        
        if (time < 64f)
        {
            time += lvl*Time.deltaTime*64f;
        }

        if (time >= 64f)
        {
            
            if (shoot(controller))
            {

                time -= 64f;
            }
        }

        if (totalTime > 704f*lvl)
        {
            Object.Destroy(controller.gameObject);
        }
    }

    public bool shoot(ProjectileController controller)
    {
        List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(controller.transform.position, (2 + (lvl > 4 ? 1 : 0)) * MapCreator.scale,LayerMask.GetMask("Enemy")));
        while (sphere.Count > 0 && sphere[0].gameObject.GetComponent<FruitCode>().hidden)
        {
            sphere.RemoveAt(0);
        }
        if (sphere.Count > 0)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.transform.localScale *= 0.5f;
            pc.code = new LifeSmallProjectile(upgrade1,upgrade2,upgrade3);
            pc.code.lvl = lvl;
            pc.code.target = sphere[0].gameObject.GetComponent<FruitCode>();
            projectile.transform.position = controller.transform.position;
            pc.material.color = controller.material.color;
            pc.code.Start(pc);
            controller.StartCoroutine(recharge(controller,64f/lvl));
            return true;
        }
        return false;
    }

    public IEnumerator recharge(ProjectileController controller, float rechargeTime)
    {
        for (float i = 1; i <= rechargeTime; i+=Time.deltaTime*64f)
        {
            controller.transform.localScale = new Vector3(i * .25f /  rechargeTime, i * .25f / rechargeTime, i * .25f / rechargeTime);
            yield return null;
        }
    }
}
