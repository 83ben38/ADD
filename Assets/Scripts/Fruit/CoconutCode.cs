using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutCode : FruitCode
{
    [SerializeField]
    public int reductionMod;
    
    public override void Damage(int amount)
    {
        hp -= amount / reductionMod;
        if (hp < 1)
        {
            StartButtonController.startButton.objects.Remove(gameObject);
            Destroy(gameObject);
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z);
    }
    
}
