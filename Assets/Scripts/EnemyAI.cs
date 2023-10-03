using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private enum State {
        WaitingForEnemyTurn = 0,
        TakingTurn = 5,
        Busy = 10
    }

    [SerializeField] private float timer;

    private State state;

    private void Awake() {
        state = State.WaitingForEnemyTurn;
    }

    private void Start() {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }


    private void Update() {
        if (TurnSystem.Instance.IsPlayerTurn) {
            return;
        }

        switch (state) {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if (timer < 0f) {
                    if (TryTakeEnemyAIAction(SetStateTakingTurn)) {
                        state = State.Busy;
                    } else {
                        //No more enemies have actions that they can take, end enemy turn
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
        }

    }

    private void SetStateTakingTurn() {
        float timeToSmoothEnemies = .5f;
        timer = timeToSmoothEnemies;
        state = State.TakingTurn;
    }
    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e) {
        if (!TurnSystem.Instance.IsPlayerTurn) {
            state = State.TakingTurn;

            float timeBetweenEnemyActions = 2f;
            timer = timeBetweenEnemyActions;
        }
    }

    private bool TryTakeEnemyAIAction(Action onEnemyAIActionComplete) {
        Debug.Log("Taking Enemy AI Action");
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList()) {
            if (TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete)) {
                return true;
            }
        }
        return false;
    }

    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete) {
        SpinAction spinAction = enemyUnit.GetSpinAction();

        GridPosition actionGridPosition = enemyUnit.GetGridPosition();

        if (!spinAction.IsValidActionGridPosition(actionGridPosition)) {
            return false;
        }

        if (!enemyUnit.TrySpendActionPointsToTakeAction(spinAction)) {
            return false;
        }

        Debug.Log("Doing Spin Action");
        spinAction.TakeAction(actionGridPosition, onEnemyAIActionComplete);
        return true;
    }
}