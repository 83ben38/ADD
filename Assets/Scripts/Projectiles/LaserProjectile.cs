using UnityEditor;
using UnityEngine;

public class LaserProjectile : ProjectileCode
{
    public TowerController start;
    public TowerController end;
    private LineRenderer laser;
    public LaserProjectile(bool upgrade1, bool upgrade2, bool upgrade3, TowerController start, TowerController end) : base(upgrade1, upgrade2, upgrade3)
    {
        this.start = start;
        this.end = end;
    }

    private Vector3 pos;
    private Vector3 pos1;
    private Vector3 pos3;
    private Vector3 pos4;
    private Vector3 dir;
    private float ticksLeft = 6f;
    public override void Start(ProjectileController controller)
    {
        base.Start(controller);
        laser = new GameObject("Laser").AddComponent<LineRenderer>();
        laser.material = new Material(controller.material);
        Color c = laser.material.color;
        laser.material.color = new Color(c.r,c.g,c.b,0.25f);
        laser.startWidth = 0.01f*lvl;
        laser.endWidth = 0.01f*lvl;
        laser.positionCount = 3;
        pos = start.transform.position;
        pos1 = end.transform.position;
        pos.y += MapCreator.scale*1.8f;
        pos1.y += MapCreator.scale*1.8f;
        laser.SetPosition(0,pos);
        laser.SetPosition(1,((pos+pos1)/2)+(Random.insideUnitSphere*(MapCreator.scale*0.5f)));
        laser.SetPosition(2, pos1);
        laser.useWorldSpace = true;
        laser.generateLightingData = true;
        laser.transform.SetParent(controller.transform);
        dir = pos - pos1;
        pos3 = new Vector3(pos.x,pos.y- (MapCreator.scale * 0.8f),pos.z);
        pos4 = new Vector3(pos1.x, pos1.y - (MapCreator.scale * 0.8f), pos1.z);
    }

    public override void tick(ProjectileController controller)
    {
        ticksLeft -= Time.deltaTime * 64f * (upgrade1 ? lvl/2f : 1);
        while (ticksLeft <= 0)
        {
            shoot();
            ticksLeft += 6;
        }
    }

    private void shoot()
    {
        laser.SetPosition(1,(((pos+pos1)/2))+(Random.insideUnitSphere*(MapCreator.scale*0.5f)));
        RaycastHit hit;
        Physics.Raycast(pos3, -dir, out hit, dir.magnitude, LayerMask.GetMask("Enemy"));
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<FruitCode>().Damage(lvl * (upgrade1 ? 3 : 1));
        }
        Physics.Raycast(pos4, dir, out hit, dir.magnitude, LayerMask.GetMask("Enemy"));
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<FruitCode>().Damage(lvl);
        }
    }
}
