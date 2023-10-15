using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingUpdater : MonoBehaviour
{

    private void Start() {
        DestructibleCrate.OnAveDestroyed += DestructibleCrate_OnAveDestroyed;
    }

    private void DestructibleCrate_OnAveDestroyed(object sender, System.EventArgs e) {
        DestructibleCrate crate = sender as DestructibleCrate;
        Pathfinding.Instance.SetIsWalkableGridPosition(crate.gridPosition, true);
    }

    private void OnDisable() {
        DestructibleCrate.OnAveDestroyed -= DestructibleCrate_OnAveDestroyed;
    }

    private void OnDestroy() {
        DestructibleCrate.OnAveDestroyed -= DestructibleCrate_OnAveDestroyed;
    }
}
