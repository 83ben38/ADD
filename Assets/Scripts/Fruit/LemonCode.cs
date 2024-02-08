using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class LemonCode : FruitCode
{
    
    [SerializeField]
    private float time = 64f;
    [SerializeField]
    public float radius;
    [SerializeField]
    public float speedAmount;
    


    public override void FixedUpdate()
    {
        base.FixedUpdate();
        time += Time.deltaTime * 64f;
        if (time >= 64f)
        {
            time = 0;
            Collider[] hit = Physics.OverlapSphere(transform.position, radius * MapCreator.scale,
                LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                FruitCode f = hit[i].gameObject.GetComponent<FruitCode>();
                if (f != this)
                {
                    StartCoroutine(hitEnemy(f));
                }
            }
        }
    }
    IEnumerator hitEnemy(FruitCode fruit)
    {
        fruit.speed += speedAmount;
        for (float i = 0; i < 64f; i+=Time.deltaTime*64f)
        {
            yield return null;
        }

        fruit.speed -= speedAmount;
    }
}
