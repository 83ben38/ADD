using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaCode : FruitCode
{
    private bool dead = false;
    public float speedIncrease;
    public override void FixedUpdate()
    {
        if (dead)
        {
            Collider[] hit = Physics.OverlapSphere(transform.position, minScale*MapCreator.scale, LayerMask.GetMask("Enemy"));
            if (hit.Length > 0)
            {
                hit[0].GetComponent<FruitCode>().speed += speedIncrease;
                Destroy(gameObject);
            }
        }
        else
        {
            base.FixedUpdate();
        }
    }

    public override void Damage(int amount)
    {
        hp -= amount + vulnerability;
        if (hp < 1)
        {
            StartButtonController.startButton.objects.Remove(gameObject);
            dead = true;
            gameObject.layer = 0;
            transform.localScale = new Vector3(minScale, minScale, minScale)*MapCreator.scale;
            return;
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z)*MapCreator.scale;
    }
}
