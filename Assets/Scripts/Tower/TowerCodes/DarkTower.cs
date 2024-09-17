using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTower : TowerCode
{
    private int statIncreaseAmount = 0;
    private float ticksLeft2;
    private bool abilityUsed = true;
    private bool pause = false;

    public DarkTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        attackSpeed = 256;
        range = 1;
    }

    public override void MouseClick(TowerController controller)
    {
        if (!abilityUsed)
        {
            GameObject projectile = Object.Instantiate(TowerCode.projectile);
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            Vector3 v3 = controller.transform.position;
            v3.y += +(MapCreator.scale * 1.5f);
            projectile.transform.position = v3;
            pc.material.color = getColor();
            pause = true;
            base.controller.StartCoroutine(runBlackHole(projectile));
        }
    }

    public IEnumerator runBlackHole(GameObject projectile)
    {
        
        float range = (statIncreaseAmount / 4f) + 1f;
        for (float i = 0; i < 1; i+=Time.deltaTime*2)
        {
            projectile.transform.localScale = new Vector3(i*MapCreator.scale,i*MapCreator.scale,i*MapCreator.scale);
            yield return null;
        }
        projectile.transform.localScale = new Vector3(MapCreator.scale,MapCreator.scale,MapCreator.scale);
        int damageDone = 0;
        for (float i = 0; i < lvl*2; i+=Time.deltaTime)
        {
            List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(self, range * MapCreator.scale,LayerMask.GetMask("Enemy")));
            for (int j = 0; j < sphere.Count; j++)
            {
                Vector3 move = (projectile.transform.position - sphere[j].gameObject.transform.position).normalized *
                    ((statIncreaseAmount / 4f) + 1f);
                sphere[j].gameObject.transform.position += move * Time.deltaTime;
            }

            int damage = (int)((i / statIncreaseAmount) - damageDone);
            sphere = new List<Collider>(Physics.OverlapSphere(self, MapCreator.scale * 0.5f,LayerMask.GetMask("Enemy")));
            for (int j = 0; j < sphere.Count; j++)
            {
                sphere[j].GetComponent<FruitCode>().Damage(damage*lvl);
            }

            damageDone += damage;
            yield return null;
        }
        Object.Destroy(projectile);
        pause = false;
    }


    public override int getAttackSpeed()
    {
        switch (statIncreaseAmount)
        {
            case 0:
            case 1: return 256;
            case 2: 
            case 3: 
            case 4: return 192;
            case 5: 
            case 6:
            case 7: 
            case 8: return 128;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13: return 96;
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:  return 64;
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26: return 48;
        }
        return 32;
    }

    public override float getRange()
    {
        switch (statIncreaseAmount)
        {
            case 0:
            case 1: return base.getRange();
            case 2: 
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8: return base.getRange()+1;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 20:
            case 21:
            case 22:
            case 23: return base.getRange()+2;
        }

        return base.getRange() + 3;
    }

    public override void tick()
    {
        int bonusStats = 0;
        if (upgrade2)
        {
            ticksLeft2 -= lvl*Time.deltaTime*4f;
            List<Collider> sphere = new List<Collider>(Physics.OverlapSphere(self, getRange() * MapCreator.scale,LayerMask.GetMask("Projectile")));
            for (int i = 0; i < sphere.Count; i++)
            {
                ProjectileController z;
                if (sphere[i].TryGetComponent(out z))
                {
                    if (z.code is DeathProjectile d)
                    {
                        d.pierceLeft -= d.lvl * (int)ticksLeft2;
                        if (d.pierceLeft < 1)
                        {
                            Object.Destroy(sphere[i].gameObject);
                        }

                        bonusStats += d.lvl;
                    }
                }
            }

            ticksLeft2 -= (int)ticksLeft2;
        }

        statIncreaseAmount += bonusStats;
        if (ticksLeft > 0 && !pause)
        {
            ticksLeft -= lvl*Time.deltaTime*64f;
        }

        if (ticksLeft <= 0)
        {
            
            if (shoot())
            {
                int attackSpeed = getAttackSpeed();
                List<TowerController> nextTo = new List<TowerController>(controller.nextTo);
                for (int k = 0; k < getRange()-1; k++)
                {
                    int z = nextTo.Count;
                    for (int i = 0; i < z; i++)
                    {

                        List<TowerController> nextToNextTo = nextTo[i].nextTo;

                        for (int j = 0; j < nextToNextTo.Count; j++)
                        {
                            if (! nextTo.Contains(nextToNextTo[j]) )
                            {
                            
                                nextTo.Add(nextToNextTo[j]);
                            
                            } 
                        }
                    }
                }
                for (int i = 0; i < nextTo.Count; i++)
                {
                    if (nextTo[i].tower != null && !(nextTo[i].tower is DarkTower) && nextTo[i].tower.rechargeTime < nextTo[i].tower.getAttackSpeed()*4)
                    {
                        nextTo[i].tower.ticksLeft += rechargeTime * nextTo[i].tower.lvl * (upgrade1 ? 0.75f : 0.5f);
                        nextTo[i].tower.rechargeTime += rechargeTime * nextTo[i].tower.lvl * (upgrade1 ? 0.75f : 0.5f);
                    }
                }
                ticksLeft = attackSpeed + ticksLeft;
                rechargeTime = attackSpeed - 1;
            }
        }

        statIncreaseAmount -= bonusStats;
    }

    public override void roundStart()
    {
        if (upgrade3)
        {
            abilityUsed = false;
        }

        base.roundStart();
        statIncreaseAmount = 0;
        List<TowerController> nextTo = new List<TowerController>(controller.nextTo);
        for (int k = 0; k < getRange()-1; k++)
        {
            int z = nextTo.Count;
            for (int i = 0; i < z; i++)
            {

                List<TowerController> nextToNextTo = nextTo[i].nextTo;

                for (int j = 0; j < nextToNextTo.Count; j++)
                {
                    if (! nextTo.Contains(nextToNextTo[j]) )
                    {
                            
                        nextTo.Add(nextToNextTo[j]);
                            
                    } 
                }
            }
        }
        for (int i = 0; i < nextTo.Count; i++)
        {
            if (nextTo[i].tower != null && !(nextTo[i].tower is DarkTower))
            {
                statIncreaseAmount += nextTo[i].tower.lvl;
            }
        }

        if (upgrade1)
        {
            statIncreaseAmount = (int)(statIncreaseAmount * 1.5f);
        }
    }

    public override ProjectileCode create()
    {
        return new DarkProjectile(upgrade1, upgrade2, upgrade3,statIncreaseAmount);
    }

    public override Color getColor()
    {
        return ColorManager.manager.darkTower;
    }

    public override object Clone()
    {
        return new DarkTower(upgrade1, upgrade2, upgrade3);
    }
}
