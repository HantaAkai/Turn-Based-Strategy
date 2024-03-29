using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAction : BaseAction {

    public int maxInteractDistance = 1;

    private void Update() {
        if (!isActive) {
            return;
        }
    }

    public override string GetActionName() {
        return "Interact";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition) {
        return new EnemyAIAction {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }

    public override List<GridPosition> GetValidActionGridPositionList() {

        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();


        for (int x = -maxInteractDistance; x <= maxInteractDistance; x++) {
            for (int z = -maxInteractDistance; z <= maxInteractDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValisdGridPosition(testGridPosition)) {
                    continue;
                }

                Door door = LevelGrid.Instance.GetDoorAtGridPosition(testGridPosition);
                if (door == null) {
                    //No Door here
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete) {
        Door door = LevelGrid.Instance.GetDoorAtGridPosition(gridPosition);
        door.Interact(OnInteractComplete);

        ActionStart(onActionComplete);
    }

    public void OnInteractComplete() {
        ActionComplete();
    }
}
