using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public LineCollision lc;
    public Transform FirePoint;
    public Transform LaserHit;
    public Transform gun;
    public TurretBehavior tower;


    int oldTick, tick;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        tick = 0;
        oldTick = tick;
        lc.DisableCollision();




        //lineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        //lineRenderer.BakeMesh(mesh, true);
        //meshCollider.sharedMesh = mesh;
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, FirePoint.position);
        //Debug.DrawLine(LaserHit.position, hit.point);
        //LaserHit.position = hit.point;

        lineRenderer.SetPosition(0, gun.transform.position);
        lineRenderer.SetPosition(1, FirePoint.position);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            lc.EnableCollision();
            lineRenderer.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            lc.DisableCollision();
            lineRenderer.enabled = false;
        }

    }



    private void OnTriggerStay2D(Collider2D collision)
    {

        EnemyBehavior e = collision.gameObject.GetComponent<EnemyBehavior>();
        if (e)
        {

            e.TakeHit(tower.GetDamage());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBehavior e = collision.gameObject.GetComponent<EnemyBehavior>();
        if (e)
        {
            Debug.Log("Hit " + e + " with laser");
            e.TakeHit(tower.GetDamage());
        }
    }



    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        return positions;
    }

    public float GetWidth()
    {
        return lineRenderer.startWidth;
    }
}
