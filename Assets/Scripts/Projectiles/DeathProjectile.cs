using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DeathProjectile : ProjectileCode
{
    public Vector3 pos;
    public int pathNum;
    public int path;
    public Vector3 goalPos;

    public DeathProjectile(bool upgrade1, bool upgrade2, bool upgrade3, DeathTower.pathData data) : base(upgrade1, upgrade2, upgrade3)
    {
        pos = data.pos;
        pathNum = data.pathNum;
        path = data.path;
        goalPos = data.goalPos;
    }

    public override int getPierce()
    {
        return lvl * lvl * 3;
    }

    public override void Start(ProjectileController controller)
    {
        base.Start(controller);
        controller.transform.localScale *= 3;
        controller.transform.position = pos;
    }

    public override void tick(ProjectileController controller)
    {
       Transform transform = controller.transform;
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
         pathNum--;
         if (pathNum == 0)
         {
            LivesController.controller.damage((int)-Math.Log(pierceLeft,2));
            Object.Destroy(controller.gameObject);
            return;
         }

         if (PathfinderManager.manager.path[path][pathNum].tileType > 2 && PathfinderManager.manager.path[path][pathNum].tileType == PathfinderManager.manager.path[path][pathNum + 1].tileType)
         {
            Vector3 v = PathfinderManager.manager.path[path][pathNum].transform.position;
            transform.position = new Vector3(v.x, transform.position.y, v.z);
            pathNum--;
            if (pathNum == 0)
            {
               LivesController.controller.damage((int)-Math.Log(pierceLeft,2));
               Object.Destroy(controller.gameObject);
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
      Collider[] hit = Physics.OverlapSphere(controller.transform.position, .75f*MapCreator.scale, LayerMask.GetMask("Enemy"));
      for (int i = 0; i < hit.Length; i++)
      {
         this.hit(hit[i].gameObject.GetComponent<FruitCode>(), controller);
      }
    }

    public override void hit(FruitCode fruit, ProjectileController controller)
    {
       int damage = Math.Min(fruit.hp, pierceLeft);
       fruit.Damage(damage);
       pierceLeft-=damage;
       if (pierceLeft < 1)
       {
          Object.Destroy(controller.gameObject);
       }
    }
}
