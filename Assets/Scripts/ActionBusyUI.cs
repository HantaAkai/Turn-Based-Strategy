using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour {

    private void Start() {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;

        UpdateVisual();
    }

    private void UnitActionSystem_OnBusyChanged(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        gameObject.SetActive(UnitActionSystem.Instance.IsBusy);
    }
}