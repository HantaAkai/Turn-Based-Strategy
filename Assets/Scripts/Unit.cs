using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour {

    private const int ACTION_POINTS_MAX = 10;

    public static event EventHandler OnAnyActionPointsChanged;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;

    public event EventHandler OnActionPointsChanged;

    [SerializeField] private bool isEnemy;
    public bool IsEnemy { get { return isEnemy; } }
    
    private GridPosition gridPosition;
    private HealthSystem healthSystem;
    private BaseAction[] baseActionArary;
    private int actionPoints = ACTION_POINTS_MAX;
    public int ActionPoints { get { return actionPoints; } }

    private void Awake() {
        baseActionArary = GetComponents<BaseAction>();
        healthSystem = GetComponent<HealthSystem>();
    }


    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position); 
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        healthSystem.OnDead += HealthSystem_OnDead;

        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void HealthSystem_OnDead(object sender, EventArgs e) {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);

        Destroy(gameObject);

        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }

    private void Update() {
        

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition) {
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;

            LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
        }

    }
    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e) {
        if ((IsEnemy && !TurnSystem.Instance.IsPlayerTurn) ||
            (!IsEnemy && TurnSystem.Instance.IsPlayerTurn)) {
            actionPoints = ACTION_POINTS_MAX;

            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
            OnActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }

    }

    public T GetAction<T>() where T : BaseAction {
        foreach (BaseAction baseAction in baseActionArary) {
            if (baseAction is T) {
                return (T)baseAction;
            }
        }
        return null;
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
        OnActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TakeDamage(int damageAmount) {
        healthSystem.Damage(damageAmount);
    }

    public Vector3 GetWorldPosition() {
        return transform.position;
    }

    public float GethealthNormalized() {
        return healthSystem.GetHealthNormalized();
    }
}



