using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start() {
        unit.OnActionPointsChanged += unit_OnAnyActionPointsChanged;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;

        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        UpdateHealthBar();
    }

    private void OnDestroy() {
        unit.OnActionPointsChanged -= unit_OnAnyActionPointsChanged;
        healthSystem.OnDamaged -= HealthSystem_OnDamaged;
    }

    private void OnDisable() {
        unit.OnActionPointsChanged -= unit_OnAnyActionPointsChanged;
        healthSystem.OnDamaged -= HealthSystem_OnDamaged;
    }

    private void unit_OnAnyActionPointsChanged(object sender, System.EventArgs e) {
        UpdateActionPointsText();
    }

    private void UpdateActionPointsText() {
        actionPointsText.text = unit.ActionPoints.ToString();
    }

    private void UpdateHealthBar() {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
}