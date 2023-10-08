using System.Collections;
using System.Collections.Generic;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UI;

public class Pathfinding : MonoBehaviour {

    public static Pathfinding Instance {get; private set; }

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private LayerMask obstaclesLayerMask;
    [SerializeField] private bool createDebugObjects;

    private int width;
    private int height;
    private float cellSize;
    private GridSystem<PathNode> gridSystem;

    private void Awake() {

        if (Instance != null) {
            Debug.LogError($"There is more than one Pathfinding! ${transform} - ${Instance}");
            Destroy(Instance);
            return;
        }
        Instance = this;

        
    }

    public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition, out int pathLength) {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        PathNode startNode = gridSystem.GetGridObject(startGridPosition);
        PathNode endNode = gridSystem.GetGridObject(endGridPosition);
        openList.Add(startNode);

        //Reset all pathNodes
        for (int x = 0; x < gridSystem.GetWidth(); x++) {
            for (int z = 0; z < gridSystem.GetHeight(); z++) {
                GridPosition gridPosition = new GridPosition(x, z);
                PathNode pathNode = gridSystem.GetGridObject(gridPosition);

                pathNode.SetGCost(int.MaxValue);
                pathNode.SetHCost(0);
                pathNode.CalculateFCost();
                pathNode.ResetCameFromPathNode();
            }
        }

        //Set starting node
        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistance(startGridPosition, endGridPosition));
        startNode.CalculateFCost();


        while (openList.Count > 0) {
            PathNode currentNode = GetLowestFCostPathNode(openList);

            if (currentNode == endNode) {
                //Reached the final node
                pathLength = endNode.fCost;
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //Cycle through neigbour nodes
            foreach (PathNode neighbourNode in GetNeighbourList(currentNode)) {
                if (closedList.Contains(neighbourNode)) {
                    continue;
                }

                if (!neighbourNode.isWalkable) {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode.gridPosition, neighbourNode.gridPosition);

                if (tentativeGCost < neighbourNode.gCost) {
                    neighbourNode.SetCameFromPathNode(currentNode);
                    neighbourNode.SetGCost(tentativeGCost);
                    neighbourNode.SetHCost(CalculateDistance(neighbourNode.gridPosition, endNode.gridPosition));
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        //No path found
        pathLength = 0;
        return null;
    }

    public int CalculateDistance(GridPosition gridPositionA, GridPosition gridPositionB) {
        GridPosition gridPositionDistance = gridPositionA - gridPositionB;

        int xDistance = Mathf.Abs(gridPositionDistance.x);
        int zDistance = Mathf.Abs(gridPositionDistance.z);

        int remaining = Mathf.Abs(xDistance - zDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList) {
        PathNode result = pathNodeList[0];

        for (int i = 1; i < pathNodeList.Count; i++) {
            if (pathNodeList[i].fCost < result.fCost) {
                result = pathNodeList[i]; 
            }
        }

        return result;
    }

    private PathNode GetNode(int x, int z) {
        return gridSystem.GetGridObject(new GridPosition(x, z));
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode) {
        List<PathNode> neighbourList = new List<PathNode>();

        GridPosition gridPosition = currentNode.gridPosition;

        if (gridPosition.x - 1 >= 0) {
            //Left
            neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 0));

            if (gridPosition.z - 1 >= 0) {
                //Left-Down
                neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z - 1));
            }

            if (gridPosition.z + 1 < gridSystem.GetHeight()) {
                //Left-Up
                neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 1));
            }
        }

        if (gridPosition.x + 1 < gridSystem.GetWidth()) {
            //Right
            neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 0));

            if (gridPosition.z - 1 >= 0) {
                //Right-Down
                neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z - 1));
            }

            if (gridPosition.z + 1 < gridSystem.GetHeight()) {
                //Right-Up
                neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 1));
            }
        }


        if (gridPosition.z - 1 >= 0) {
            //Down
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z - 1));
        }

        if (gridPosition.z + 1 < gridSystem.GetHeight()) {
            //Up
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z + 1));
        }

        return neighbourList;
    }

    private List<GridPosition> CalculatePath(PathNode endNode) {
        List<PathNode> pathNodeList = new List<PathNode>();
        pathNodeList.Add(endNode);

        PathNode currentNode = endNode;

        while (currentNode.cameFromPathNode != null) {
            pathNodeList.Add(currentNode.cameFromPathNode);
            currentNode = currentNode.cameFromPathNode;
        }

        pathNodeList.Reverse();

        List<GridPosition> gridPositionList = new List<GridPosition>();
        foreach (PathNode pathNode in pathNodeList) {
            gridPositionList.Add(pathNode.gridPosition);
        }

        return gridPositionList;
    }

    public void Setup(int width, int height, float cellSize) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridSystem = new GridSystem<PathNode>(width, height, cellSize, (GridSystem<PathNode> g, GridPosition gridPosition) => new PathNode(gridPosition));

        //Creates debug objects
        if (createDebugObjects) {
            gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        }

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                GridPosition gridPosition = new GridPosition(x, z);
                Vector3 worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

                float raycastOffsetDistance = 5f;
                if (Physics.Raycast(worldPosition + Vector3.down * raycastOffsetDistance, Vector3.up, raycastOffsetDistance * 2, obstaclesLayerMask)) {
                    GetNode(x, z).SetIsWalkable(false);
                }
            }
        }
    }

    public bool IsWalkableGridPosition(GridPosition gridPosition) {
        return gridSystem.GetGridObject(gridPosition).isWalkable;
    }

    public bool HasPath(GridPosition startGridPosition, GridPosition endGridPosition) {
        return FindPath(startGridPosition, endGridPosition, out int pathLength) != null;
    }

    public int GetPathLength(GridPosition startGridPosition, GridPosition endGridPosition) {
        FindPath(startGridPosition, endGridPosition, out int pathLength);
        return pathLength;
    }
}