using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutCode : FruitCode
{
    [SerializeField]
    public int reductionMod;
    
    public override void Damage(int amount)
    {
        if (amount + vulnerability - reductionMod < 0)
        {
            return;
        }

        hp -= amount + vulnerability - reductionMod;
        if (hp < 1)
        {
            StartButtonController.startButton.objects.Remove(gameObject);
            Destroy(gameObject);
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z)*MapCreator.scale;
    }
    
}
