using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeProjectile : ProjectileCode
{
    public new Vector3 target;
    public Vector3 start;
    private float time = -64f;
    private float totalTime;
    private TowerCode projectileGenerator;
    private float attackSpeed;

    public LifeProjectile(bool upgrade1, bool upgrade2, bool upgrade3, TowerCode projectileGenerator, int lvl) : base(upgrade1, upgrade2, upgrade3)
    {
        this.projectileGenerator = projectileGenerator;
        
        if (projectileGenerator is LifeTower)
        {
            attackSpeed = 96f;
        }
        else
        {
            attackSpeed = projectileGenerator.getAttackSpeed() * 2 * (float)projectileGenerator.lvl / lvl;
        }

        this.lvl = lvl;
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
        
        if (time < attackSpeed)
        {
            time += lvl*Time.deltaTime*64f;
        }

        if (time >= attackSpeed)
        {
            
            if (shoot(controller))
            {

                time -= attackSpeed;
            }
        }

        if (totalTime > 704f*lvl*(upgrade1?3f:1f))
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
            pc.code = projectileGenerator.create();
            pc.code.lvl = projectileGenerator.lvl > 1 ? lvl : 2;
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
        for (float i = 1; i <= rechargeTime; i+=Time.deltaTime*96f)
        {
            controller.transform.localScale = new Vector3(i * .25f * MapCreator.scale /  rechargeTime, i * .25f * MapCreator.scale / rechargeTime, i * .25f * MapCreator.scale / rechargeTime);
            yield return null;
        }
    }
}
