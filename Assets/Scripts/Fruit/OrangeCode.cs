using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeCode : FruitCode
{
    public override void Damage(int amount)
    {
        hp -= amount;
        if (hp < 1)
        {
            //split
            Destroy(gameObject);
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z);
    }
}
