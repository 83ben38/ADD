using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearCode : FruitCode
{
    public List<TowerController> selfPath;
    private float tempSpeedMult = 1f;
    void Start()
    {
        selfPath = PathfinderManager.manager.requestPearPath(PathfinderManager.manager.path[path][0]);
        if (selfPath[1].block)
        {
           goalPos = selfPath[0].transform.position;
           goalPos.y += MapCreator.scale * 1.5f;
           tempSpeedMult = 0.5f;
           return;
        }

        goalPos = selfPath[1].transform.position;
        goalPos.y += MapCreator.scale;
    }
   public override void FixedUpdate()
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
      Vector3 unit = goal.normalized;
      float newSpeed = speed*Time.deltaTime*64*tempSpeedMult;
      Vector3 div = new Vector3(goal.x / unit.x, goal.y / unit.y, goal.z / unit.z);
      if (!float.IsFinite(div.x))
      {
         div.x = 0;
      }
      if (!float.IsFinite(div.y))
      {
         div.y = 0;
      }
      if (!float.IsFinite(div.z))
      {
         div.z = 0;
      }

      while (div.magnitude < newSpeed)
      {
         newSpeed -= div.magnitude;
         transform.Translate(unit.x * div.magnitude, 0, unit.y * div.magnitude);
         if (pathNum + 1 >= selfPath.Count)
         {
            LivesController.controller.damage((int)Math.Log(hp, 2));
            StartButtonController.startButton.objects.Remove(gameObject);
            Destroy(gameObject);
            return;
         }

         if (selfPath[pathNum + 1].block &&
             goalPos.y - selfPath[pathNum].transform.position.y < 1.5f * MapCreator.scale)
         {
            tempSpeedMult = 0.5f;
            goalPos.y += MapCreator.scale * 0.5f;
            goal = goalPos - transform.position;
            unit = goal.normalized;
            div = new Vector3(goal.x / unit.x, goal.y / unit.y, goal.z / unit.z);
            div.x = 0;
            div.z = 0;
            continue;
         }

         if (!selfPath[pathNum].block && goalPos.y - selfPath[pathNum].transform.position.y > MapCreator.scale * 1.1f && tempSpeedMult == 1f)
         {
            tempSpeedMult = 0.5f;
            goalPos.y -= MapCreator.scale * 0.5f;
            goal = goalPos - transform.position;
            unit = goal.normalized;
            div = new Vector3(goal.x / unit.x, goal.y / unit.y, goal.z / unit.z);
            div.x = 0;
            div.z = 0;
            continue;
         }

         pathNum++;
         tempSpeedMult = 1f;

         if (selfPath[pathNum].tileType > 2 && selfPath[pathNum].tileType == selfPath[pathNum - 1].tileType)
         {
            Vector3 v = selfPath[pathNum].transform.position;
            transform.position = new Vector3(v.x, transform.position.y, v.z);
            pathNum++;
            if (pathNum >= selfPath.Count)
            {
               LivesController.controller.damage((int)Math.Log(hp,2));
               StartButtonController.startButton.objects.Remove(gameObject);
               Destroy(gameObject);
               return;
            }
         }
         goalPos = selfPath[pathNum].transform.position;
         goalPos.y  = transform.position.y;
         goal = goalPos - transform.position;
         unit = goal.normalized;
         div = new Vector3(goal.x / unit.x, goal.y / unit.y, goal.z / unit.z);
         if (!float.IsFinite(div.x))
         {
            div.x = 0;
         }
         if (!float.IsFinite(div.y))
         {
            div.y = 0;
         }
         if (!float.IsFinite(div.z))
         {
            div.z = 0;
         }
      }
      transform.Translate(unit.x*newSpeed,unit.y*newSpeed,unit.z*newSpeed);
   }
    
}
