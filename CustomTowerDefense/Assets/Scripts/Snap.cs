
using UnityEngine;

public class Snap : MonoBehaviour
{
    //[SerializeField] private Vector3 gridSize = default;
    [SerializeField] private Vector3 gridSize = new Vector3(.25f, .25f, .25f);
    private void OnDrawGizmos()
    {
        SnapToGrid2();
    }
    public void SnapToGrid()
    {
        var position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x),
            Mathf.RoundToInt(this.transform.position.y),
            Mathf.RoundToInt(this.transform.position.z)
            );
        this.transform.position = position;
    }
    public Vector3 SnapToGrid(Vector3 position)
    {
        position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
            Mathf.RoundToInt(this.transform.position.y / this.gridSize.y) * this.gridSize.y,
            0
            );
        //this.transform.position = position;
        return position;
    }

    private void SnapToGrid2()
    {
        var position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
            Mathf.RoundToInt(this.transform.position.y / this.gridSize.y) * this.gridSize.y,
            Mathf.RoundToInt(this.transform.position.z / this.gridSize.z) * this.gridSize.z
            );
        this.transform.position = position;
    }
}
