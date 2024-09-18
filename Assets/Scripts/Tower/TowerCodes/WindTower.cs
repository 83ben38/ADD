using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class WindTower : TowerCode
{
    private List<TowerController> buffed = new List<TowerController>();
    private List<TowerController> nextTo = new List<TowerController>();
    private float buffValue = 1f;
    private bool abilityUsed = false;
    public WindTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1,upgrade2, upgrade3) {
        range = 1;
        attackSpeed = 64;
    }

    public override void MouseClick(TowerController controller)
    {
        if (!abilityUsed)
        {
            Vector3 pos = controller.transform.position;
            List<GameObject> windProjectiles = new List<GameObject>();
            for (int i = 0; i < 3; i++)
            {
                
                GameObject g = Object.Instantiate(TowerCode.projectile);
                g.GetComponent<ProjectileController>().material.color = getColor();
                g.transform.localScale *= ((i + 2) / 3f)*3*MapCreator.scale;
                pos.y += (i + 2) * MapCreator.scale / 6f;
                g.transform.position = pos;
                windProjectiles.Add(g);
            }

            controller.StartCoroutine(runWhirlwind(windProjectiles));
        }
    }

    public IEnumerator runWhirlwind(List<GameObject> windProjectiles)
    {
        Vector3 currentPos = controller.transform.position;
        currentPos.y = MapCreator.scale;
        for (float i = 0; i < 10; i+=Time.deltaTime)
        {
            RaycastHit hit;
            Ray ray = MouseManager.manager.cameraTransform.ScreenPointToRay(UnityEngine.Input.mousePosition);
            Physics.Raycast(ray, out hit,1000);
            Vector3 v3 = hit.transform.position;
            v3.y = MapCreator.scale;
            Vector3 move = (v3 - currentPos).normalized;
            move *= Time.deltaTime * 5;
            currentPos += move;
            for (int j = 0; j < windProjectiles.Count; j++)
            {
                Vector3 newPos = currentPos;
                newPos.y += (j + 2) / 6f;
                float value = (i / (j + 3)) * 30;
                newPos += new Vector3((float)Math.Sin(value)*MapCreator.scale/3f, 0, (float)Math.Cos(value)*MapCreator.scale/3f);
                windProjectiles[j].transform.position = newPos;
            }
            Collider[] enemiesHit = Physics.OverlapSphere(currentPos, MapCreator.scale, LayerMask.GetMask("Enemy"));
            for (int j = 0; j < enemiesHit.Length; j++)
            {
                enemiesHit[j].transform.position += move;
            }
            yield return null;
        }

        for (int i = 0; i < windProjectiles.Count; i++)
        {
            Object.Destroy(windProjectiles[i]);
        }
    }

    public override int getAttackSpeed()
    {
        return 64 * lvl;
    }

    

    public override ProjectileCode create()
    {
        return new WindProjectile(upgrade1,upgrade2,upgrade3,controller);
    }

    public override Color getColor()
    {
       return ColorManager.manager.windTower;
    }

    public override object Clone()
    {
        return new WindTower(upgrade1, upgrade2, upgrade3);
    }

    public override bool shoot()
    {
        buffValue = getAttackSpeed() * (upgrade2 ? .8f : 1) / rechargeTime;
        SoundEffectsManager.manager.playSound("wind");
        for (int i = 0; i < buffed.Count; i++)
        {
            if (buffed[i].tower != null)
            {
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = new WindProjectile(upgrade1,upgrade2,upgrade3,buffed[i]);
                pc.code.lvl = lvl > 1 ? lvl : 2;
                projectile.transform.position = controller.towerVisual.shoot(getAttackSpeed()-1);
                pc.material.color = getColor();
                pc.code.Start(pc);
            }
        }

        buffed = new List<TowerController>();
        return true;
    }

    public override void tick()
    {
        base.tick();


        List<TowerController> toBuff = new List<TowerController>();

        for (int i = 0; i < nextTo.Count; i++)
        {
            if (nextTo[i].tower != null && !(nextTo[i].tower is WindTower) && nextTo[i].tower.ticksLeft > 0)
            {
                toBuff.Add(nextTo[i]);
                if (!buffed.Contains(nextTo[i]))
                {
                    buffed.Add(nextTo[i]);
                }
            }
        }

        for (int i = 0; i < toBuff.Count; i++)
        {
            toBuff[i].tower.ticksLeft -= Time.deltaTime * lvl * 64f / (upgrade1 ? toBuff.Count : 3f) * buffValue  * (upgrade3 ? toBuff[i].tower.getAttackSpeed() / 128f : 1f);
            toBuff[i].tower.rechargeTime -= Time.deltaTime * lvl * 64f / (upgrade1 ? toBuff.Count : 3f) * buffValue * (upgrade3 ? toBuff[i].tower.getAttackSpeed() / 128f : 1f);
        }


    }

    public override void roundStart()
    {
        if (upgrade2)
        {
            abilityUsed = false;
        }

        base.roundStart();
        nextTo = new List<TowerController>(controller.nextTo);
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
    }
}
