using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystem : MonoBehaviour {

    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    public event EventHandler OnSelectedUnitChanged;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update() {


        if (Input.GetMouseButtonDown(0)) {
            if (TryHandleUnitSelection()) return;

            selectedUnit.Move(MouseWorld.GetPosition());            
        }
    }

    private bool TryHandleUnitSelection() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask)) {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit)) {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
            
        }
        return false;
    }

    private void SetSelectedUnit(Unit newUnit) {
        selectedUnit = newUnit;

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit() {
        return selectedUnit;
    }
}