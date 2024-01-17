using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class FruitCode : MonoBehaviour
{
   public int hp;
   private int maxHp;
   public float speed;
   private int pathNum = 1;
   private Vector3 goalPos;
   public float maxScale;
   public float minScale;
   public float frozenTime = 0f;
   void Start()
   {
      maxHp = hp;
      Vector3 v = PathfinderManager.manager.path[0].transform.position;
      goalPos = PathfinderManager.manager.path[1].transform.position;
      transform.position = new Vector3(v.x, v.y + 1, v.z);
      transform.localScale = new Vector3(maxScale, maxScale, maxScale);
   }

   public void Damage(int amount)
   {
      hp -= amount;
      if (hp < 1)
      {
         Destroy(gameObject);
      }

      float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
      transform.localScale = new Vector3(z, z, z);
   }

   void FixedUpdate()
   {
      if (speed <= 0)
      {
         frozenTime += Time.deltaTime;
         if (frozenTime >= 10)
         {
            Destroy(gameObject);
         }

         return;
      }

      frozenTime = 0f;
      Vector3 goal = goalPos - transform.position;
      Vector2 diff = new Vector2(goal.x, goal.z);
      Vector2 unit = diff.normalized;
      float newSpeed = speed*Time.deltaTime*64;
      Vector2 div = (diff / unit);
      if (!float.IsFinite(div.x))
      {
         div.x = 0;
      }
      if (!float.IsFinite(div.y))
      {
         div.y = 0;
      }
      while (div.magnitude < newSpeed)
      {
         newSpeed -= div.magnitude;
         transform.Translate(unit.x*div.magnitude,0,unit.y*div.magnitude);
         pathNum++;
         if (pathNum >= PathfinderManager.manager.path.Count)
         {
            LivesController.controller.damage((int)Math.Log(hp,2));
            Destroy(gameObject);
            return;
         }
         goalPos = PathfinderManager.manager.path[pathNum].transform.position;
         goal = goalPos - transform.position;
         diff = new Vector2(goal.x, goal.z);
         unit = diff.normalized;
         div = (diff / unit);
         if (!float.IsFinite(div.x))
         {
            div.x = 0;
         }
         if (!float.IsFinite(div.y))
         {
            div.y = 0;
         }
      }
      transform.Translate(unit.x*newSpeed,0,unit.y*newSpeed);
   }
}
