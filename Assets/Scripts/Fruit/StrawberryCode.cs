using UnityEngine;

public class StrawberryCode : FruitCode
{
    public float stunAmount;
    public float stunRadius;

    public override void Damage(int amount)
    {
        hp -= amount + vulnerability;
        if (hp < 1)
        {
            StartButtonController.startButton.objects.Remove(gameObject);
            Collider[] hit = Physics.OverlapSphere(transform.position, stunRadius*MapCreator.scale);
            for (int i = 0; i < hit.Length; i++)
            {
                TowerController tc = hit[i].GetComponent<TowerController>();
                if (tc != null && tc.tower != null)
                {
                    tc.tower.ticksLeft += stunAmount * tc.tower.lvl;
                }
            }
            Destroy(gameObject);
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z)*MapCreator.scale;
    }
}
