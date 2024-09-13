using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WatermelonCode : BossCode
{
    public int phase;
    public GameObject splitObject;
    public EnemyConfiguration splitConfig;
    //phase 1
    public int damageReduction;
    // phase 1 + 4
    public int healAmount;

    public float healRadius;
    //phase 2
    public float stunAmount;
    public float stunRange;
    // phase 2 + 3
    public int summon1Amount;
    public int summon2Amount;
    public GameObject summon1;
    public GameObject summon2;
    public EnemyConfiguration config1;
    public EnemyConfiguration config2;
    // phase 4
    public float speedAmount;
    public override void doAttack()
    {
        if (phase == 1)
        {
            hp += healAmount;
            if (hp > maxHp)
            {
                hp = maxHp;
            }

            float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
            transform.localScale = new Vector3(z, z, z)*MapCreator.scale;
        }

        if (phase == 2)
        {
            int attackNum = Random.Range(1, 4);
            if (attackNum == 1)
            {
                Collider[] hit = Physics.OverlapSphere(transform.position, stunRange*MapCreator.scale);
                List<TowerController> stunned = new List<TowerController>();
                for (int i = 0; i < hit.Length; i++)
                {
                    TowerController tc = hit[i].GetComponentInParent<TowerController>();
                    if (tc != null && tc.tower != null && ! stunned.Contains(tc))
                    {
                        tc.tower.ticksLeft += stunAmount * tc.tower.lvl;
                        if (tc.tower.ticksLeft > (stunAmount * tc.tower.lvl) + tc.tower.getAttackSpeed())
                        {
                            tc.tower.ticksLeft = (stunAmount * tc.tower.lvl) + tc.tower.getAttackSpeed();
                        }
                        stunned.Add(tc);
                    }
                }
            }

            if (attackNum == 2)
            {
                for (int i = 0; i < summon1Amount; i++)
                {
                    GameObject o = Instantiate(summon1);
                    Vector2 r = Random.insideUnitCircle*0.75f*MapCreator.scale;
                    Vector3 currentPos = transform.position;
                    o.transform.position = new Vector3(currentPos.x + r.x, currentPos.y, currentPos.z + r.y);
                    FruitCode f = o.GetComponent<FruitCode>();
                    f.pathNum = pathNum;
                    f.goalPos = goalPos;
                    f.path = path;
                    f.hp = config1.hp;
                    f.maxHp = config1.hp;
                    f.speed = config1.speed/1000f;
                    f.minScale = config1.minSize/1000f;
                    f.maxScale = config1.maxSize/1000f;
                    f.transform.localScale = new Vector3(f.maxScale, f.maxScale, f.maxScale)*MapCreator.scale;
                    config1.runOptions(f);
                    StartButtonController.startButton.objects.Add(o);
                }
            }
            if (attackNum == 3)
            {
                for (int i = 0; i < summon2Amount; i++)
                {
                    GameObject o = Instantiate(summon2);
                    Vector2 r = Random.insideUnitCircle*0.75f*MapCreator.scale;
                    Vector3 currentPos = transform.position;
                    o.transform.position = new Vector3(currentPos.x + r.x, currentPos.y, currentPos.z + r.y);
                    FruitCode f = o.GetComponent<FruitCode>();
                    f.pathNum = pathNum;
                    f.goalPos = goalPos;
                    f.path = path;
                    f.hp = config2.hp;
                    f.maxHp = config2.hp;
                    f.speed = config2.speed/1000f;
                    f.minScale = config2.minSize/1000f;
                    f.maxScale = config2.maxSize/1000f;
                    f.transform.localScale = new Vector3(f.maxScale, f.maxScale, f.maxScale)*MapCreator.scale;
                    config2.runOptions(f);
                    StartButtonController.startButton.objects.Add(o);
                }
            }
        }
        if (phase == 3)
        {
            int attackNum = Random.Range(2, 4);
            if (attackNum == 2)
            {
                for (int i = 0; i < summon1Amount; i++)
                {
                    GameObject o = Instantiate(summon1);
                    Vector2 r = Random.insideUnitCircle*0.75f*MapCreator.scale;
                    Vector3 currentPos = transform.position;
                    o.transform.position = new Vector3(currentPos.x + r.x, currentPos.y, currentPos.z + r.y);
                    FruitCode f = o.GetComponent<FruitCode>();
                    f.pathNum = pathNum;
                    f.goalPos = goalPos;
                    f.path = path;
                    f.hp = config1.hp;
                    f.maxHp = config1.hp;
                    f.speed = config1.speed/1000f;
                    f.minScale = config1.minSize/1000f;
                    f.maxScale = config1.maxSize/1000f;
                    f.transform.localScale = new Vector3(f.maxScale, f.maxScale, f.maxScale)*MapCreator.scale;
                    config1.runOptions(f);
                    StartButtonController.startButton.objects.Add(o);
                }
            }
            if (attackNum == 3)
            {
                for (int i = 0; i < summon2Amount; i++)
                {
                    GameObject o = Instantiate(summon2);
                    Vector2 r = Random.insideUnitCircle*0.75f*MapCreator.scale;
                    Vector3 currentPos = transform.position;
                    o.transform.position = new Vector3(currentPos.x + r.x, currentPos.y, currentPos.z + r.y);
                    FruitCode f = o.GetComponent<FruitCode>();
                    f.pathNum = pathNum;
                    f.goalPos = goalPos;
                    f.path = path;
                    f.hp = config2.hp;
                    f.maxHp = config2.hp;
                    f.speed = config2.speed/1000f;
                    f.minScale = config2.minSize/1000f;
                    f.maxScale = config2.maxSize/1000f;
                    f.transform.localScale = new Vector3(f.maxScale, f.maxScale, f.maxScale)*MapCreator.scale;
                    config2.runOptions(f);
                    StartButtonController.startButton.objects.Add(o);
                }
            }
        }

        if (phase == 4)
        {
            int attackNum = Random.Range(1, 3);
            if (attackNum == 1)
            {
                speed += speedAmount / 1000f;
            }
            else
            {
                
                Collider[] hit = Physics.OverlapSphere(transform.position, healRadius * MapCreator.scale,
                    LayerMask.GetMask("Enemy"));
                for (int i = 0; i < hit.Length; i++)
                {
                    FruitCode f = hit[i].gameObject.GetComponent<FruitCode>();
                    f.hp += healAmount; 
                    if (f.hp > f.maxHp)
                    {
                        f.hp = f.maxHp;
                    }
                    float x = ((maxScale - minScale) * ((float)f.hp / maxHp))   + minScale;
                    f.transform.localScale = new Vector3(x,x,x)*MapCreator.scale;
                }
            }
        }
    }

    public override void Damage(int amount)
    {
        if (amount + (int)(vulnerability/vulnerabilityDecrease) - damageReduction < 0)
        {
            return;
        }

        hp -= amount + (int)(vulnerability/vulnerabilityDecrease) - damageReduction;
        if (hp < 1)
        {
            if (phase != 4)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject o = Instantiate(splitObject);
                    Vector2 r = Random.insideUnitCircle * 0.75f * MapCreator.scale;
                    Vector3 currentPos = transform.position;
                    o.transform.position = new Vector3(currentPos.x + r.x, currentPos.y, currentPos.z + r.y);
                    FruitCode f = o.GetComponent<FruitCode>();
                    f.pathNum = pathNum;
                    f.goalPos = goalPos;
                    f.path = path;
                    f.hp = splitConfig.hp;
                    f.maxHp = splitConfig.hp;
                    f.speed = splitConfig.speed / 1000f;
                    f.minScale = splitConfig.minSize / 1000f;
                    f.maxScale = splitConfig.maxSize / 1000f;
                    f.transform.localScale = new Vector3(f.maxScale, f.maxScale, f.maxScale) * MapCreator.scale;
                    splitConfig.runOptions(f);
                    StartButtonController.startButton.objects.Add(o);
                }
            }

            StartButtonController.startButton.objects.Remove(gameObject);
            Destroy(gameObject);
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z)*MapCreator.scale;
    }
}
