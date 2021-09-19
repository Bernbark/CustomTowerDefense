using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrid : MonoBehaviour
{
    public Transform cursor;
    private GridHelper grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new GridHelper(.2f);
    }

    // Update is called once per frame
    void Update()
    {
        cursor.position = GameUtils.Instance.GetMouseWorldPositionForUtils();
        
        if (Input.GetMouseButtonDown(0))
        {
            
            grid.SetValue(GameUtils.GetMouseWorldPosition(), 56);
        }
    }
}
