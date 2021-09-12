using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper {

    
    private const int MAX_WIDTH = 81;
    private const int MAX_HEIGHT = 43;
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public GridHelper(float cellSize)
    {
        this.cellSize = cellSize;
        gridArray = new int[MAX_WIDTH, MAX_HEIGHT];
        debugTextArray = new TextMesh[MAX_WIDTH, MAX_HEIGHT];

        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                debugTextArray[x,y] = GameUtils.CreateWorldText(gridArray[x, y].ToString(), GetWorldPosition(x, y, cellSize), null, 20, Color.white, TextAnchor.MiddleCenter);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, MAX_HEIGHT), GetWorldPosition(MAX_WIDTH,MAX_HEIGHT), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(MAX_WIDTH, 0), GetWorldPosition(MAX_WIDTH,MAX_HEIGHT), Color.white, 100f);

        SetValue(2, 1, 56);
    }
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3((x - MAX_WIDTH/2) * cellSize, (y - MAX_HEIGHT/2) * cellSize);
    }

    private Vector3 GetWorldPosition(int x, int y, float cellSize)
    {
        return new Vector3((x-MAX_WIDTH/2) * cellSize, (y-MAX_HEIGHT/2) * cellSize);
    }
    
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize*2);
        y = Mathf.FloorToInt(worldPosition.y / cellSize*2);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < MAX_WIDTH && y < MAX_HEIGHT)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
}
