using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeActions : MonoBehaviour {

    [SerializeField] private float shootIntensity = .5f;
    [SerializeField] private float grenadeIntensity = 2f;

    private void Start() {
        ShootAction.OnAnyShoot += ShootAction_OnAnyShoot;
        GrenadeProjectile.OnAnyGrenadeExploded += GrenadeProjectile_OnAnyGrenadeExploded;
    }

    private void GrenadeProjectile_OnAnyGrenadeExploded(object sender, System.EventArgs e) {
        ScreenShake.Instance.Shake(grenadeIntensity);
    }

    private void OnDestroy() {
        ShootAction.OnAnyShoot -= ShootAction_OnAnyShoot;
        GrenadeProjectile.OnAnyGrenadeExploded -= GrenadeProjectile_OnAnyGrenadeExploded;
    }

    private void OnDisable() {
        ShootAction.OnAnyShoot -= ShootAction_OnAnyShoot;
        GrenadeProjectile.OnAnyGrenadeExploded -= GrenadeProjectile_OnAnyGrenadeExploded;
    }

    private void ShootAction_OnAnyShoot(object sender, ShootAction.OnShootEventArgs e) {
        ScreenShake.Instance.Shake(shootIntensity);
    }
}