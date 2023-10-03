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
                    state = State.Busy;
                    TakeEnemyAIAction(SetStateTakingTurn);
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

    private void TakeEnemyAIAction(Action onEnemyAIActionComplete) {
        Debug.Log("Taking Enemy AI Action");
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList()) {
            TakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete);
        }
    }

    private void TakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete) {
        SpinAction spinAction = enemyUnit.GetSpinAction();

        GridPosition actionGridPosition = enemyUnit.GetGridPosition();

        if (!spinAction.IsValidActionGridPosition(actionGridPosition)) {
            return;
        }

        if (enemyUnit.TrySpendActionPointsToTakeAction(spinAction)) {
            Debug.Log("Doing Spin Action");
            spinAction.TakeAction(actionGridPosition, onEnemyAIActionComplete);
        }
    }
}