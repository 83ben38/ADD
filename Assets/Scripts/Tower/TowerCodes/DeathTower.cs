using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTower : TowerCode
{
    [SerializeField]
    private List<GameObject> inRange;
    private List<pathData> rangePositions;

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
    }

    public override void MouseClick(TowerController controller)
    {
       
    }

    public override void tick()
    {
        for (int i = 0; i < inRange.Count; i++)
        {
            if (inRange[i] == null)
            {
                GameObject projectile = Object.Instantiate(TowerCode.projectile);
                ProjectileController pc = projectile.GetComponent<ProjectileController>();
                pc.code = new DeathProjectile(upgrade1,upgrade2,upgrade3,rangePositions[i]);
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
