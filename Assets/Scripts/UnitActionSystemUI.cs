using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour {

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainer;

    private void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        CreateUnitActionButtons();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, System.EventArgs e) {
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons() {
        foreach (Transform buttonTransform in actionButtonContainer) {
            Destroy(buttonTransform.gameObject);
        }

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray()) {
            Instantiate(actionButtonPrefab, actionButtonContainer);
        }
    }
}