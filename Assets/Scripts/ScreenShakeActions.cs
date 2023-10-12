using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeActions : MonoBehaviour {

    public void Start() {
        ShootAction.OnAnyShoot += ShootAction_OnAnyShoot;
    }

    private void OnDestroy() {
        ShootAction.OnAnyShoot -= ShootAction_OnAnyShoot;
    }

    private void OnDisable() {
        ShootAction.OnAnyShoot -= ShootAction_OnAnyShoot;
    }

    private void ShootAction_OnAnyShoot(object sender, ShootAction.OnShootEventArgs e) {
        ScreenShake.Instance.Shake();
    }
}