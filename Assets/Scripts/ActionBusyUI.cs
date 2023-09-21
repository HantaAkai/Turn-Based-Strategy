using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour {

    private void Start() {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;
        Hide();
    }

    private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy) {
        if (isBusy) {
            Show();
        } else {
            Hide();
        }
    }
    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}