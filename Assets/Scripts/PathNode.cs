using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {


    private GridPosition gridPosition;
    public int gCost { get; private set; }
    public int hCost { get; private set; }
    public int fCost { get; private set; }
    private PathNode cameFromPathNode;

    public PathNode(GridPosition gridPosition) {
        this.gridPosition = gridPosition;
    }

    public override string ToString() {
        return gridPosition.ToString();
    }

}