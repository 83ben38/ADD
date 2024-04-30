using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossCode : FruitCode
{
   public float attackSpeed;
   private float ticksLeft;
   public override void OnEnable()
   {
      base.OnEnable();
      ticksLeft = attackSpeed;
   }

   public override void Damage(int amount)
   {
      hp -= amount + (vulnerability/3);
      if (hp < 1)
      {
         StartButtonController.startButton.objects.Remove(gameObject);
         Destroy(gameObject);
      }

      float z = ((maxScale - minScale) * ((float)hp / maxHp))   + minScale;
      transform.localScale = new Vector3(z, z, z)*MapCreator.scale;
   }
    public override void FixedUpdate()
   {
      if (base.speed > 0)
      {
         frozenTime = 0f;
      }
      float speed = base.speed < 0.5f ? 0.5f : base.speed;
      Vector3 goal = goalPos - transform.position;
      Vector2 diff = new Vector2(goal.x, goal.z);
      Vector2 unit = diff.normalized;
      float newSpeed = speed*Time.deltaTime;
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
      ticksLeft -= Time.deltaTime;
      if (ticksLeft <= 0)
      {
         doAttack();
         ticksLeft = attackSpeed;
      }
   }

    public abstract void doAttack();
}
