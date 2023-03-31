using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour {

    [SerializeField] private Unit unit;
    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
         
    }

    private void Start() {       
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit) {
            Show();
        } else {
            Hide();
        }
    }


    private void Show() {
        meshRenderer.enabled = true;
    }

    private void Hide() {
        meshRenderer.enabled = false;
    }
}