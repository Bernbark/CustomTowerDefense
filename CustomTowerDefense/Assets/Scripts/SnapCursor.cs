
using UnityEngine;

public class SnapCursor : MonoBehaviour
{
    [SerializeField] private Vector3 gridSize = new Vector3(.25f,.25f,.25f);
    private void Update()
    {
        this.transform.position = GameUtils.Instance.GetMouseWorldPositionForUtils();
        SnapToGrid();
    }
    private void OnDrawGizmos()
    {
        
    }
    private void SnapToGrid()
    {
        var position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
            Mathf.RoundToInt(this.transform.position.y / this.gridSize.y) * this.gridSize.y,
            Mathf.RoundToInt(this.transform.position.z / this.gridSize.z) * this.gridSize.z
            );
        this.transform.position = position;
    }
    
}
