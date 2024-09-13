using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FruitCode : MonoBehaviour
{
   public int path;
   public int hp;
   public int maxHp;
   public float speed;
   public int pathNum = 1;
   public Vector3 goalPos;
   public float maxScale;
   public float minScale;
   public int vulnerability = 0;
   public float frozenTime = 0f;

   public bool hidden = false;
   public virtual void OnEnable()
   {
      speed *= MapCreator.scale;
      minScale *= MapCreator.scale;
      maxScale *= MapCreator.scale;
      maxHp = hp;
      transform.localScale = new Vector3(maxScale, maxScale, maxScale);
   }

   public virtual void Damage(int amount)
   {
      hp -= amount + vulnerability;
      if (hp < 1)
      {
         StartButtonController.startButton.objects.Remove(gameObject);
         Destroy(gameObject);
      }

      float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
      transform.localScale = new Vector3(z, z, z)*MapCreator.scale;
   }

   public virtual void FixedUpdate()
   {
      if (speed <= 0)
      {
         frozenTime += Time.deltaTime;
         if (frozenTime >= 10)
         {
            StartButtonController.startButton.objects.Remove(gameObject);
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
         if (pathNum >= PathfinderManager.manager.path[path].Count)
         {
            LivesController.controller.damage((int)Math.Log(hp,2));
            StartButtonController.startButton.objects.Remove(gameObject);
            Destroy(gameObject);
            return;
         }

         if (PathfinderManager.manager.path[path][pathNum].tileType > 2 && PathfinderManager.manager.path[path][pathNum].tileType == PathfinderManager.manager.path[path][pathNum - 1].tileType)
         {
            Vector3 v = PathfinderManager.manager.path[path][pathNum].transform.position;
            transform.position = new Vector3(v.x, transform.position.y, v.z);
            pathNum++;
            if (pathNum >= PathfinderManager.manager.path[path].Count)
            {
               LivesController.controller.damage((int)Math.Log(hp,2));
               StartButtonController.startButton.objects.Remove(gameObject);
               Destroy(gameObject);
               return;
            }
         }

         goalPos = PathfinderManager.manager.path[path][pathNum].transform.position;
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
