using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueberryCode : FruitCode
{
    public float speedIncrease;
    public override void Damage(int amount)
    {
        hp -= amount + vulnerability;
        if (hp < 1)
        {
            StartButtonController.startButton.objects.Remove(gameObject);
            Destroy(gameObject);
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z)*MapCreator.scale;
        speed += speedIncrease;
    }
}
