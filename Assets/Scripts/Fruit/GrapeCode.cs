using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;


public class GrapeCode : FruitCode
{
    public float spawnCooldown;
    public GameObject spawnObject;
    public EnemyConfiguration spawnConfig;
    private float cooldownAmount;
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        cooldownAmount += Time.deltaTime;
        if (cooldownAmount >= spawnCooldown)
        {
            cooldownAmount -= spawnCooldown;
            GameObject o = Instantiate(spawnObject);
            o.transform.position = transform.position;
            FruitCode f = o.GetComponent<FruitCode>();
            f.pathNum = pathNum;
            f.goalPos = goalPos;
            f.path = path;
            f.hp = spawnConfig.hp;
            f.maxHp = spawnConfig.hp;
            f.speed = spawnConfig.speed/1000f;
            f.minScale = spawnConfig.minSize/1000f;
            f.maxScale = spawnConfig.maxSize/1000f;
            f.transform.localScale = new Vector3(f.maxScale, f.maxScale, f.maxScale)*MapCreator.scale;
            spawnConfig.runOptions(f);
            StartButtonController.startButton.objects.Add(o);
        }
    }
}
