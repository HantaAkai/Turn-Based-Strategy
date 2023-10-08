using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathfindingGridDebugObject : GridDebugObject {

    [SerializeField] private TextMeshPro gCostText;
    [SerializeField] private TextMeshPro hCostText;
    [SerializeField] private TextMeshPro fCostText;
    [SerializeField] private SpriteRenderer isWalkableSpriteRenderer;

    private PathNode pathNode;

    public override void SetGridObject(object gridObject) {
        base.SetGridObject(gridObject);

        pathNode = (PathNode)gridObject;
    }

    protected override void Update() {
        base.Update();
        gCostText.text = pathNode.gCost.ToString();
        hCostText.text = pathNode.hCost.ToString();
        fCostText.text = pathNode.fCost.ToString();
        isWalkableSpriteRenderer.color = pathNode.isWalkable ? Color.green : Color.red;
    }
}