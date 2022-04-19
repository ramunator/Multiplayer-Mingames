using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Gameplay/Grid/Building")]
public class Bulding : ScriptableObject
{
    public enum Type
    {
        Roof,
        Wall,
        Floor,
        Funature
    };

    public static Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.down: return Dir.left;
            case Dir.left: return Dir.up;
            case Dir.up: return Dir.right;
            case Dir.right: return Dir.down;
        }
    }

    public enum Dir
    {
        up,
        down,
        left,
        right
    };

    public string buildingName;
    public Sprite icon;
    public Type buildingType;
    public MeshFilter visualMeshFilter;
    public Transform prefab;
    public Transform visual;
    public int width;
    public int height;

    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.down: return 0;
            case Dir.left: return 90;
            case Dir.up: return 180;
            case Dir.right: return 270;
        }
    }

    public Vector2Int GetRoationOffset(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.down: return new Vector2Int(0, 0);
            case Dir.left: return new Vector2Int(0, width);
            case Dir.up: return new Vector2Int(width, height);
            case Dir.right: return new Vector2Int(height, 0);
        }
    }

    public List<Vector2Int> GetGridPosList(Vector2Int offset, Dir dir)
    {
        List<Vector2Int> gridPosList = new List<Vector2Int>();
        switch (dir)
        {
            default:
            case Dir.down:
            case Dir.up:
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        gridPosList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
            case Dir.left:
            case Dir.right:
                for (int x = 0; x < height; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        gridPosList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
        }
        return gridPosList;
    }
}
