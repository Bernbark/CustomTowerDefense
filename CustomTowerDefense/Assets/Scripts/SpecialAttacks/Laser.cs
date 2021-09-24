using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public LineCollision lc;
    private Transform enemyTransform;
   
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
        if (enemyTransform != null)
        {
            lineRenderer.SetPosition(0, tower.transform.position);
            lineRenderer.SetPosition(1, enemyTransform.position);
            lc.EnableCollision();
            lineRenderer.enabled = true;
        }
        
    }

    public void SetEnemy(EnemyBehavior enemy)
    {
        this.enemyTransform = enemy.transform;
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
