using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using CodeMonkey.MonoBehaviours;
using CodeMonkey.Utils;
using System;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem Instance { get; private set; }

    public event EventHandler OnSelectedChanged;

    public InputMaster controls;

    public List<Bulding> buildingsList;
    public Bulding building;

    private Bulding.Dir dir = Bulding.Dir.down;

    public int x = 10;
    public int z = 10;

    public GameObject originPos;
    public float cellSize = 5f;
    public LayerMask ground;

    private Grid<GridObject> grid;
    private Grid<GridObject> roofGrid;

    public List<GameObject> lastPlacedObjects = new List<GameObject>();



    private void Awake()
    {
        grid = new Grid<GridObject>(x, z, cellSize, originPos.transform.position, (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));

        roofGrid = new Grid<GridObject>(x, z, cellSize, originPos.transform.position + new Vector3(0, 3.5f, 0), (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));

        Instance = this;

        controls = new InputMaster();

        controls.BuildMode.PlaceBuilding.performed += ctx => PlaceBuilding();

        controls.BuildMode.DestroyBuilding.performed += ctx => DestroyBuilding();
    }

    #region EnableControls

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    #endregion

    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x;
        private int z;
        private PlacedObject placedObject;

        public GridObject(Grid<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetPlacedObject(PlacedObject placedObject)
        {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, z);
        }

        public PlacedObject GetPlacedObject()
        {
            return placedObject;
        }

        public bool CanBuild()
        {
            return placedObject == null;
        }

        public void ClearPlacedObject()
        {
            placedObject = null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + placedObject;
        }
    }

    public Vector3 GetMouseWorldSnappedPosition()
    {
        Vector3 mousePosition = GetMouseWorldPos();

        if(building.buildingType == Bulding.Type.Roof) 
        {
            roofGrid.GetXZ(mousePosition, out int x, out int z);

            if (building != null)
            {
                Vector2Int rotationOffset = building.GetRoationOffset(dir);
                Vector3 placedObjectWorldPosition = roofGrid.GetWorldPos(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * roofGrid.GetCellSize();
                return placedObjectWorldPosition;
            }
            else
            {
                return mousePosition;
            }

        }
        else if(building.buildingType == Bulding.Type.Wall || building.buildingType == Bulding.Type.Floor) 
        { 
            grid.GetXZ(mousePosition, out int x, out int z);

            if (building != null)
            {
                Vector2Int rotationOffset = building.GetRoationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPos(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
                return placedObjectWorldPosition;
            }
            else
            {
                return mousePosition;
            }
        }
        else { return Vector3.zero; }
    }

    public Quaternion GetPlacedObjectRotation()
    {
        if (building != null)
        {
            return Quaternion.Euler(0, building.GetRotationAngle(dir), 0);
        }
        else
        {
            return Quaternion.identity;
        }
    }

    public Bulding GetPlacedObjectTypeSO()
    {
        return building;
    }

    public void PlaceBuilding()
    {
        if (GameManager.Instance.buildState != GameManager.BuildState.buildMode) { return; }
        if (building == null) { return; }

        if (GameManager.Instance.buildMode && building.buildingType == Bulding.Type.Wall || building.buildingType == Bulding.Type.Floor)
        {
            grid.GetXZ(GetMouseWorldPos(), out int x, out int z);

            if (!grid.IsValid(x, z)) { return; }

            lastPlacedObjects.Clear();

            List<Vector2Int> gridPosList = building.GetGridPosList(new Vector2Int(x, z), dir);

            bool canBuild = true;
            foreach (Vector2Int gridPos in gridPosList)
            {
                if (!grid.GetGridObject(gridPos.x, gridPos.y).CanBuild())
                {
                    canBuild = false;
                    break;
                }
            }


            GridObject gridObject = grid.GetGridObject(x, z);
            if (canBuild)
            {
                Vector2Int rotationOffset = building.GetRoationOffset(dir);
                Vector3 placedObjectWorldPos = grid.GetWorldPos(x, z)
                    + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPos, new Vector2Int(x, z), dir, building);


                foreach (Vector2Int gridPos in gridPosList)
                {
                    grid.GetGridObject(gridPos.x, gridPos.y).SetPlacedObject(placedObject);
                }

                lastPlacedObjects.Add(placedObject.gameObject);
            }

            else
            {
                UtilsClass.CreateWorldTextPopup(transform, "Cannot Build Here!", GetMouseWorldPos(), 5, Color.white, GetMouseWorldPos() + new Vector3(0, 2, 0), .5f, new Vector3(90, 0, 0));
            }

        }
        else
        {
            roofGrid.GetXZ(GetMouseWorldPos(), out int x, out int z);

            if (!roofGrid.IsValid(x, z)) { return; }

            lastPlacedObjects.Clear();

            List<Vector2Int> gridPosList = building.GetGridPosList(new Vector2Int(x, z), dir);

            bool canBuild = true;
            foreach (Vector2Int gridPos in gridPosList)
            {
                if (!grid.GetGridObject(gridPos.x, gridPos.y).CanBuild())
                {
                    canBuild = false;
                    break;
                }
            }


            GridObject gridObject = grid.GetGridObject(x, z);
            if (canBuild)
            {
                Vector2Int rotationOffset = building.GetRoationOffset(dir);
                Vector3 placedObjectWorldPos = roofGrid.GetWorldPos(x, z)
                    + new Vector3(rotationOffset.x, 0, rotationOffset.y) * roofGrid.GetCellSize();

                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPos, new Vector2Int(x, z), dir, building);


                foreach (Vector2Int gridPos in gridPosList)
                {
                    grid.GetGridObject(gridPos.x, gridPos.y).SetPlacedObject(placedObject);
                }

                lastPlacedObjects.Add(placedObject.gameObject);
            }

            else
            {
                UtilsClass.CreateWorldTextPopup(transform, "Cannot Build Here!", GetMouseWorldPos(), 5, Color.white, GetMouseWorldPos() + new Vector3(0, 2, 0), .5f, new Vector3(90, 0, 0));
            }
        }
    }

    public void DestroyBuilding()
    {
        if(GameManager.Instance.buildState != GameManager.BuildState.DemolishMode) { return; }

        GridObject gridObject = grid.GetGridObject(GetMouseWorldPos());
        PlacedObject placedObject = gridObject.GetPlacedObject();
        if(placedObject != null)
        {
            placedObject.DestroySelf();

            List<Vector2Int> gridPosList = placedObject.GetGridPosList();

            foreach (Vector2Int gridPos in gridPosList)
            {
                grid.GetGridObject(gridPos.x, gridPos.y).ClearPlacedObject();
                
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            dir = Bulding.GetNextDir(dir);
            UtilsClass.CreateWorldTextPopup("" + dir, GetMouseWorldPos());
        }



        //while (Mouse.current.leftButton.isPressed)
        //{
        //    PlaceBuilding();
        //}
    }

    private void DeselectObjectType()
    {
        building = null; RefreshSelectedObjectType();
    }

    private void RefreshSelectedObjectType()
    {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ReturnBuilding()
    {
        if(lastPlacedObjects.Count <= 0) { return; }

        foreach (GameObject _object in lastPlacedObjects)
        {
            _object.SetActive(false);
            lastPlacedObjects.Clear();
        }
    }

    public Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(ray, out RaycastHit hit, 300f, ground))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
