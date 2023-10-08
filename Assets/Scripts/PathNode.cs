using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {


    public GridPosition gridPosition { get; private set; }
    public int gCost { get; private set; }
    public int hCost { get; private set; }
    public int fCost { get; private set; }
    public PathNode cameFromPathNode { get; private set; }
    public bool isWalkable {get; private set; } = true;

    public PathNode(GridPosition gridPosition) {
        this.gridPosition = gridPosition;
    }

    public override string ToString() {
        return gridPosition.ToString();
    }

    public void SetGCost(int gCost) {
        this.gCost = gCost;
    }

    public void SetHCost(int hCost) {
        this.hCost = hCost;
    }

    public void CalculateFCost() {
        fCost = gCost + hCost;
    }

    public void ResetCameFromPathNode() {
        cameFromPathNode = null;
    }

    public void SetCameFromPathNode(PathNode pathNode) {
        cameFromPathNode = pathNode;
    }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
    }
}