using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAnimator : MonoBehaviour {

    [SerializeField] private Animator animator;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;

    private void Awake() {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction)) {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction)) {
            shootAction.OnShoot += ShootAction_OnShoot;
        } 
        
        if (TryGetComponent<SwordAction>(out SwordAction swordAction)) {
            swordAction.OnSwordActionStarted += SwordAction_OnSwordActionStarted;
            swordAction.OnSwordActionCompleted += SwordAction_OnSwordActionCompleted;
        }
    }

    private void SwordAction_OnSwordActionCompleted(object sender, System.EventArgs e) {
        
    }

    private void SwordAction_OnSwordActionStarted(object sender, System.EventArgs e) {
        animator.SetTrigger("SwordSlash");
    }

    private void ShootAction_OnShoot(object sender, ShootAction.OnShootEventArgs e) {
        animator.SetTrigger("Shoot");

        Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

        Vector3 targetUnitShootAtPosition = e.targetUnit.GetWorldPosition();
        targetUnitShootAtPosition.y = shootPointTransform.position.y;
        bulletProjectile.SetUp(targetUnitShootAtPosition);
    }

    private void MoveAction_OnStopMoving(object sender, System.EventArgs e) {
        animator.SetBool("IsWalking", false);
    }

    private void MoveAction_OnStartMoving(object sender, System.EventArgs e) {
        animator.SetBool("IsWalking", true);
    }

    private void OnDisable() {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction)) {
            moveAction.OnStartMoving -= MoveAction_OnStartMoving;
            moveAction.OnStopMoving -= MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction)) {
            shootAction.OnShoot -= ShootAction_OnShoot;
        }

        if (TryGetComponent<SwordAction>(out SwordAction swordAction)) {
            swordAction.OnSwordActionStarted -= SwordAction_OnSwordActionStarted;
            swordAction.OnSwordActionCompleted -= SwordAction_OnSwordActionCompleted;
        }
    }

    private void OnDestroy() {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction)) {
            moveAction.OnStartMoving -= MoveAction_OnStartMoving;
            moveAction.OnStopMoving -= MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction)) {
            shootAction.OnShoot -= ShootAction_OnShoot;
        }

        if (TryGetComponent<SwordAction>(out SwordAction swordAction)) {
            swordAction.OnSwordActionStarted -= SwordAction_OnSwordActionStarted;
            swordAction.OnSwordActionCompleted -= SwordAction_OnSwordActionCompleted;
        }
    }
}