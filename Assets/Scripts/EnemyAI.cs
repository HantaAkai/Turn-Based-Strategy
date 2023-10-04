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
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList()) {
            if (TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete)) {
                return true;
            }
        }
        return false;
    }

    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete) {

        EnemyAIAction bestEnemyAIAction = null;
        BaseAction bestBaseAction = null;

        foreach (BaseAction baseAction in enemyUnit.GetBaseActionArray()) {
            if (!enemyUnit.CanSpendActionPointsToTakeAction(baseAction)) {
                //Not enough AP
                continue;
            }

            if (bestEnemyAIAction == null) {
                bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                bestBaseAction = baseAction;
            } else {
                EnemyAIAction testEnemyAIAction = baseAction.GetBestEnemyAIAction();

                if (testEnemyAIAction != null && testEnemyAIAction.actionValue > bestEnemyAIAction.actionValue) {
                    bestEnemyAIAction = testEnemyAIAction;
                    bestBaseAction = baseAction;
                }
            }
        }

        if (bestEnemyAIAction != null && enemyUnit.TrySpendActionPointsToTakeAction(bestBaseAction)) {
            bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition, onEnemyAIActionComplete);
            return true;
        } else {
            return false;
        }
    }
}