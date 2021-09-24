using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Laser), typeof(PolygonCollider2D), typeof(LineRenderer))]
public class LineCollision : MonoBehaviour
{
    Laser lz;
    //public Transform tower;
    PolygonCollider2D polygonCollider;
    List<Vector2> colliderPoints = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        lz = GetComponent<Laser>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        colliderPoints = CalculateColliderPoints();
        polygonCollider.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
    }

    public void DisableCollision()
    {
        polygonCollider.enabled = false;
    }

    public void EnableCollision()
    {
        polygonCollider.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (colliderPoints != null)
        {
            colliderPoints.ForEach(p => Gizmos.DrawSphere(p, 0.1f));
        }
    }

    private List<Vector2> CalculateColliderPoints()
    {
        Vector3[] positions = lz.GetPositions();

        float width = lz.GetWidth();

        float slope = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (slope / Mathf.Pow(slope * slope + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + slope * slope, 0.5f));

        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        //Generate Collider Vertices
        List<Vector2> colliderPositions = new List<Vector2>
        {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]

        };
        return colliderPositions;
    }
}
