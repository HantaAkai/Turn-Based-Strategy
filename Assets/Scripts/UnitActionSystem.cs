using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystem : MonoBehaviour {

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

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
                    selectedUnit = unit;
                    return true;
                }
            }
            
        }
        return false;
    }
}