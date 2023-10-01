using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCameraGameObject;

    private void Start() {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;

        HideActionCamera();
    }

    private void BaseAction_OnAnyActionCompleted(object sender, System.EventArgs e) {
        switch (sender) {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }

    private void BaseAction_OnAnyActionStarted(object sender, System.EventArgs e) {
        switch (sender) {
            case ShootAction shootAction:
                ShowActionCamera();
                break;
        }
    }

    private void OnDestroy() {
        BaseAction.OnAnyActionStarted -= BaseAction_OnAnyActionStarted;
    }

    private void OnDisable() {
        BaseAction.OnAnyActionStarted -= BaseAction_OnAnyActionStarted;
    }

    private void ShowActionCamera() {
        actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera() {
        actionCameraGameObject.SetActive(false);
    }
}
