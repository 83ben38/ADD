using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoCode : FruitCode
{
    
    [SerializeField]
    public float radius;
    [SerializeField]
    public int HealAmount;
    
    public override void Damage(int amount)
    {
      
        hp -= amount+vulnerability;
        if (hp <= 0)
        {
            
            Collider[] hit = Physics.OverlapSphere(transform.position, radius * MapCreator.scale,
                LayerMask.GetMask("Enemy"));
            for (int i = 0; i < hit.Length; i++)
            {
                FruitCode f = hit[i].gameObject.GetComponent<FruitCode>();
                if (f != this)
                {
                    f.hp += HealAmount;
                    if (f.hp > f.maxHp)
                    {
                        f.hp = f.maxHp;
                    }
                    float x = ((maxScale - minScale) * ((float)f.hp / maxHp))   + minScale;
                    f.transform.localScale = new Vector3(x,x,x)*MapCreator.scale;
                }
            }
            StartButtonController.startButton.objects.Remove(gameObject);
            Destroy(gameObject);
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z);
    }
}
