using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAction : BaseAction {

    private enum State {
        SwingingSwordBeforeHit,
        SwingingSwordAfterHit,
    }

    public int maxSwordDistance { get; private set; } = 1;

    private State state;
    private float stateTimer;
    private Unit targetUnit;

    private void Update() {
        if (!isActive) {
            return;
        }


        stateTimer -= Time.deltaTime;

        switch (state) {
            case State.SwingingSwordBeforeHit:
                Vector3 aimDirection = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;

                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * rotateSpeed);
                break;
            case State.SwingingSwordAfterHit:
                break;
        }

        if (stateTimer <= 0f) {
            NextState();
        }

    }

    private void NextState() {
        switch (state) {
            case State.SwingingSwordBeforeHit:
                state = State.SwingingSwordAfterHit;
                float afterHitStateTime = .5f;
                stateTimer = afterHitStateTime;
                targetUnit.TakeDamage(100);
                break;
            case State.SwingingSwordAfterHit:
                ActionComplete();
                break;
        }
    }

    public override string GetActionName() {
        return "Sword";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition) {
        return new EnemyAIAction {
            gridPosition = gridPosition,
            actionValue = 200
        };
    }

    public override List<GridPosition> GetValidActionGridPositionList() {

        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();


        for (int x = -maxSwordDistance; x <= maxSwordDistance; x++) {
            for (int z = -maxSwordDistance; z <= maxSwordDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValisdGridPosition(testGridPosition)) {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) {
                    //GridPosition is empty, no unit there
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy == unit.IsEnemy) {
                    //both units are on the same side
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete) {
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        state = State.SwingingSwordBeforeHit;
        float beforeHitStateTime = .7f;
        stateTimer = beforeHitStateTime;

        ActionStart(onActionComplete);
    }
}