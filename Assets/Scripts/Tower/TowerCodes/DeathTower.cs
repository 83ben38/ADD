using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTower : TowerCode
{
    [SerializeField]
    private List<GameObject> inRange;
    private List<pathData> rangePositions;
    private FruitCode target;
    private pathData targetData;
    private LineRenderer lineRenderer;
    public class pathData
    {
        public Vector3 pos;
        public int pathNum;
        public int path;
        public Vector3 goalPos;

        public pathData(FruitCode fc)
        {
            pos = fc.transform.position;
            pathNum = fc.pathNum;
            path = fc.path;
            goalPos = fc.goalPos;
        }
    }

    public DeathTower(bool upgrade1, bool upgrade2, bool upgrade3) : base(upgrade1, upgrade2, upgrade3)
    {
        inRange = new List<GameObject>();
        range = 3;
        ticksLeft = 0.125f;
    }

    public override void MouseClick(TowerController controller)
    {
       
    }

    public override void tick()
    {
        if (!upgrade1)
        {
            for (int i = 0; i < inRange.Count; i++)
            {
                if (inRange[i] == null)
                {
                    GameObject projectile = Object.Instantiate(TowerCode.projectile);
                    ProjectileController pc = projectile.GetComponent<ProjectileController>();
                    pc.code = new DeathProjectile(upgrade1, upgrade2, upgrade3, rangePositions[i]);
                    pc.code.lvl = lvl;
                    pc.material.color = getColor();
                    pc.code.Start(pc);
                }
            }

            inRange = new List<GameObject>();
            rangePositions = new List<pathData>();
            List<GameObject> allFruits = StartButtonController.startButton.objects;
            Vector3 currentPos = controller.transform.position;
            for (int i = 0; i < allFruits.Count; i++)
            {
                if ((currentPos - allFruits[i].transform.position).magnitude <= getRange())
                {
                    FruitCode fc = allFruits[i].GetComponent<FruitCode>();
                    inRange.Add(fc.gameObject);
                    rangePositions.Add(new pathData(fc));
                }
            }
        }
        else
        {
            ticksLeft -= lvl * Time.deltaTime;
            if (target == null)
            {
                if (targetData != null)
                {
                    GameObject projectile = Object.Instantiate(TowerCode.projectile);
                    ProjectileController pc = projectile.GetComponent<ProjectileController>();
                    pc.code = new DeathProjectile(upgrade1, upgrade2, upgrade3, targetData);
                    pc.code.lvl = lvl;
                    pc.material.color = getColor();
                    pc.code.Start(pc);
                    targetData = null;
                }
            }

            if (ticksLeft <= 0)
            {
                ticksLeft += 0.125f;
                if (lineRenderer != null)
                {
                    Object.Destroy(lineRenderer);
                }

                if (target != null &&
                    (controller.transform.position - target.transform.position).magnitude > getRange())
                {
                    target = null;
                    targetData = null;
                }

                if (target == null)
                {
                    List<GameObject> allFruits = StartButtonController.startButton.objects;
                    Vector3 currentPos = controller.transform.position;
                    for (int i = 0; i < allFruits.Count; i++)
                    {
                        if ((currentPos - allFruits[i].transform.position).magnitude <= getRange())
                        {
                            FruitCode fc = allFruits[i].GetComponent<FruitCode>();
                            target = fc;
                            break;
                        }
                    }
                }

                if (target != null)
                {
                    target.Damage(1);
                    if (target != null)
                    {
                        lineRenderer = new GameObject("Death Lifeforce").AddComponent<LineRenderer>();
                        lineRenderer.material.color = getColor();
                        lineRenderer.startWidth = 0.1f;
                        lineRenderer.endWidth = 0.1f;
                        lineRenderer.positionCount = 2;
                        Vector3 pos = controller.transform.position;
                        pos.y += MapCreator.scale * 1.5f;
                        lineRenderer.SetPosition(0, pos);
                        lineRenderer.SetPosition(1, target.transform.position);
                        lineRenderer.useWorldSpace = true;
                        lineRenderer.generateLightingData = true;
                        lineRenderer.transform.SetParent(controller.transform);
                        targetData = new pathData(target);
                    }
                }
            }
        }
    }

    public override ProjectileCode create()
    {
        return null;
    }

    public override Color getColor()
    {
        return ColorManager.manager.deathTower;
    }

    public override object Clone()
    {
        return new DeathTower(upgrade1, upgrade2, upgrade3);
    }
}
