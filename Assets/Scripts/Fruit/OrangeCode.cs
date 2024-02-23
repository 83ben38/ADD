using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;


public class OrangeCode : FruitCode
{
    public int splitAmount;
    public GameObject splitObject;
    public EnemyConfiguration splitConfig;
    public override void Damage(int amount)
    {
        hp -= amount+vulnerability;
        if (hp < 1)
        {
            //split
            for (int i = 0; i < splitAmount; i++)
            {
                GameObject o = Instantiate(splitObject);
                Vector2 r = Random.insideUnitCircle*0.75f*MapCreator.scale;
                Vector3 currentPos = transform.position;
                o.transform.position = new Vector3(currentPos.x + r.x, currentPos.y, currentPos.z + r.y);
                FruitCode f = o.GetComponent<FruitCode>();
                f.pathNum = pathNum;
                f.goalPos = goalPos;
                f.path = path;
                f.hp = splitConfig.hp;
                f.maxHp = splitConfig.hp;
                f.speed = splitConfig.speed/1000f;
                f.minScale = splitConfig.minSize/1000f;
                f.maxScale = splitConfig.maxSize/1000f;
                f.transform.localScale = new Vector3(f.maxScale, f.maxScale, f.maxScale)*MapCreator.scale;
                splitConfig.runOptions(f);
                StartButtonController.startButton.objects.Add(o);
            }
            StartButtonController.startButton.objects.Remove(gameObject);
            Destroy(gameObject);
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z)*MapCreator.scale
            ;
    }
}
