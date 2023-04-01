using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem {

    private int width;
    private int height;
    private float cellSise;
    private GridObject[,] gridObjectArray;

    public GridSystem(int width, int height, float cellSise) {
        this.width = width;
        this.height = height;
        this.cellSise = cellSise;

        gridObjectArray = new GridObject[width, height];
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                GridPosition gridPosition = new GridPosition(x, z);

                gridObjectArray[x,z] = new GridObject(this, gridPosition);
            }
        }

        this.cellSise = cellSise;
    }


    public Vector3 GetWorldPosition(GridPosition gridPosition) { 
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSise;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSise),
            Mathf.RoundToInt(worldPosition.z / cellSise)
        );
    }

    public void CreateDebugObjects(Transform debugPrefab) {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition) {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

}