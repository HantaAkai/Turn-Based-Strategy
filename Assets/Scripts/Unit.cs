using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour {

    private const int ACTION_POINTS_MAX = 2;

    public static event EventHandler OnAnyActionPointsChanged;

    [SerializeField] private bool isEnemy;
    public bool IsEnemy { get { return isEnemy; } }
    
    private GridPosition gridPosition;
    private HealthSystem healthSystem;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActionArary;
    private int actionPoints = ACTION_POINTS_MAX;
    public int ActionPoints { get { return actionPoints; } }

    private void Awake() {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArary = GetComponents<BaseAction>();
        healthSystem = GetComponent<HealthSystem>();
    }


    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position); 
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e) {
        Destroy(gameObject);
    }

    private void Update() {
        

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition) {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }
    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e) {
        if ((IsEnemy && !TurnSystem.Instance.IsPlayerTurn) ||
            (!IsEnemy && TurnSystem.Instance.IsPlayerTurn)) {
        actionPoints = ACTION_POINTS_MAX;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);

        }

    }

    public MoveAction GetMoveAction() {
        return moveAction;
    } 
    
    public SpinAction GetSpinAction() {
        return spinAction;
    }

    public GridPosition GetGridPosition() {
        return gridPosition;
    }

    public BaseAction[] GetBaseActionArray() {
        return baseActionArary;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction) {
        if (CanSpendActionPointsToTakeAction(baseAction)) {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        } else {
            return false;
        }
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction) {
        if (actionPoints >= baseAction.GetActionPointsCost()) {
            return true;
        }
        return false;
    }

    private void SpendActionPoints(int amount) {
        actionPoints -= amount;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Damage(int damageAmount) {
        healthSystem.Damage(damageAmount);
    }

    public Vector3 GetWorldPosition() {
        return transform.position;
    }
}



