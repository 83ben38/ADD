using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;

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
            //
            //[Debug.Log("Hit " + hit.Length + " Objects.");
            List<TowerController> stunned = new List<TowerController>();
            for (int i = 0; i < hit.Length; i++)
            {
                TowerController tc = hit[i].GetComponentInParent<TowerController>();
                if (tc != null && tc.tower != null && ! stunned.Contains(tc))
                {
                    //Debug.Log("Stunning " + tc.tower.self);
                    tc.tower.ticksLeft += stunAmount * tc.tower.lvl;
                    if (tc.tower.ticksLeft > (stunAmount * tc.tower.lvl) + tc.tower.getAttackSpeed())
                    {
                        tc.tower.ticksLeft = (stunAmount * tc.tower.lvl) + tc.tower.getAttackSpeed();
                    }

                    stunned.Add(tc);
                }
            }
            Destroy(gameObject);
        }

        float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
        transform.localScale = new Vector3(z, z, z)*MapCreator.scale;
    }
}
