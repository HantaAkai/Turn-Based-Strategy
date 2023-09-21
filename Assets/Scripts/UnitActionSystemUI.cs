using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour {

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainer;

    private void Start() {
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons() {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray()) {
            Instantiate(actionButtonPrefab, actionButtonContainer);
        }
    }
}