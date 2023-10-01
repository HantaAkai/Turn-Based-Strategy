using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitWorldUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit unit;

    private void Start() {
        unit.OnActionPointsChanged += unit_OnAnyActionPointsChanged;

        UpdateActionPointsText();
    }

    private void OnDestroy() {
        unit.OnActionPointsChanged -= unit_OnAnyActionPointsChanged;
    }

    private void OnDisable() {
        unit.OnActionPointsChanged -= unit_OnAnyActionPointsChanged;
    }

    private void unit_OnAnyActionPointsChanged(object sender, System.EventArgs e) {
        UpdateActionPointsText();
    }

    private void UpdateActionPointsText() {
        actionPointsText.text = unit.ActionPoints.ToString();
    }
}