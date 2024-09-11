using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class IceTower : TowerCode
{
    public bool abilityUsed = false;
    public IceTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        range = 1;
        
    }

    public override void MouseClick(TowerController controller)
    {
        if (upgrade2 && !abilityUsed)
        {
            abilityUsed = true;
            Vector3 v3 = controller.transform.position;
            v3.y += MapCreator.scale;
            Collider[] hit = Physics.OverlapSphere(v3, (getRange()+1)*MapCreator.scale, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                controller.StartCoroutine(hitEnemy(hit[i].GetComponent<FruitCode>()));
            }
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            pc.enabled = false;
            projectile.transform.position = v3;
            Color d = getColor();
            d.a = 0.5f;
            pc.material.color = d;
            controller.StartCoroutine(expand(projectile));
        }
    }

    IEnumerator expand(GameObject go)
    {
        for (float i = 0; i < (getRange()+1)*MapCreator.scale; i+=Time.deltaTime*4)
        {
            go.transform.localScale = new Vector3(i, i, i);
            yield return null;
        }
        Object.Destroy(go);
    }

    IEnumerator hitEnemy(FruitCode fruit)
    {
        Debug.Log("HIT");
        float z = fruit.speed;
        fruit.speed -= z;
        fruit.vulnerability += lvl * 3;
        for (float i = 0; i < lvl; i+=Time.deltaTime)
        {
            yield return null;
        }
        fruit.vulnerability -= lvl * 3;
        fruit.speed += z;
    }

    public override void roundStart()
    {
        abilityUsed = false;
        base.roundStart();
    }


    public override ProjectileCode create()
    {
        return new IceProjectile(upgrade1,upgrade2,upgrade3);
    }

    public override Color getColor()
    {
        return ColorManager.manager.iceTower;
    }

    public override object Clone()
    {
        return new IceTower(upgrade1, upgrade2, upgrade3);
    }
}
