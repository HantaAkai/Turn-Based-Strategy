using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem {

    private int width;
    private int height;
    private float cellSise;

    public GridSystem(int width, int height, float cellSise) {
        this.width = width;
        this.height = height;
        this.cellSise = cellSise;

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * .2f, Color.white, 1000);
            }
        }

        this.cellSise = cellSise;
    }


    public Vector3 GetWorldPosition(int x, int z) { 
        return new Vector3(x, 0, z) * cellSise;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSise),
            Mathf.RoundToInt(worldPosition.z / cellSise)
        );
    }

}