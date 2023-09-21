using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction {

    private float spinProgress;

    private void Update() {
        if (!isActive) {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        spinProgress += spinAddAmount;

        if (spinProgress >= 360) {
            isActive = false;
            onActionComplete();
        }
    }


    public override void TakeAction(GridPosition gridPosition, Action onActionComplete) {
        this.onActionComplete = onActionComplete;
        isActive = true;
        spinProgress = 0;
    }

    public override string GetActionName() {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition> {
            unitGridPosition
        };
    }

    public override int GetActionPointsCost() {
        return 2;
    }
}