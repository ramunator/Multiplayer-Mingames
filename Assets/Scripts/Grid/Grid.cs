using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class Grid<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPos;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;

    

    public Grid(int width, int height, float cellSize, Vector3 originPos, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new TGridObject[width, height];

        bool showDebug = true;
        if (showDebug)
        {
            debugTextArray = new TextMesh[width, height];
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < gridArray.GetLength(1); z++)
                {
                    gridArray[x, z] = createGridObject(this, x, z);
                }
            }

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < gridArray.GetLength(1); z++)
                {
                    //debugTextArray[x, z] = UtilsClass.CreateWorldText(gridArray[x, z].ToString(), null, GetWorldPos(x, z) + new Vector3(cellSize, 0, cellSize) / 2, 10, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPos(x, z), GetWorldPos(x, z + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPos(x, z), GetWorldPos(x + 1, z), Color.white, 100f);
                }
                Debug.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPos(width, 0), GetWorldPos(width, height), Color.white, 100f);
            }
        }
    }

    public bool IsValid(int x, int z)
    {
        if(GetGridObject(x, z) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 GetWorldPos(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize + originPos;
    }

    public void GetXZ(Vector3 worldPos, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
        z = Mathf.FloorToInt((worldPos - originPos).z / cellSize);
    }

    public void SetGridObject(int x, int z, TGridObject value)
    {
        if(x >= 0 && z >= 0 && x < width && z < height)
        {
            gridArray[x, z] = value;
            debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
    }

    public void SetGridObject(Vector3 worldPos, TGridObject value)
    {
        int x, z;
        GetXZ(worldPos, out x, out z);
        SetGridObject(x, z, value);
    }

    public TGridObject GetGridObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return gridArray[x, z];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPos)
    {
        int x, z;
        GetXZ(worldPos, out x, out z);
        return GetGridObject(x, z);
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public void TriggerGridObjectChanged(int x, int z)
    {
        if(OnGridObjectChanged != null)
        {
            OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, z = z });
        }
    }

}
