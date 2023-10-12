using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour {

    public static event EventHandler OnAnyGrenadeExploded;

    [SerializeField] private Transform grenadeExplosionVFXPrefab;
    [SerializeField] private TrailRenderer trailRenderer;

    private Vector3 targetPosition;
    private float affectedRadius = 4f;
    private Action onGrenadeBehaviourComplete;

    private void Update() {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float moveSpeed = 15f;
        transform.position += moveDirection * Time.deltaTime * moveSpeed;

        float reachedTargetDistance = .2f;
        if (Vector3.Distance(transform.position, targetPosition) < reachedTargetDistance) {
            Collider[] colliderArray = Physics.OverlapSphere(targetPosition, affectedRadius);

            foreach (Collider collider in colliderArray) {
                if(collider.TryGetComponent<Unit>(out Unit targetUnit)) {
                    targetUnit.TakeDamage(30);
                }
            }

            OnAnyGrenadeExploded?.Invoke(this, EventArgs.Empty);

            trailRenderer.transform.parent = null;

            Vector3 grenadeVerticalOffset = Vector3.up * 1f;
            Instantiate(grenadeExplosionVFXPrefab, targetPosition + grenadeVerticalOffset, Quaternion.identity);
            
            Destroy(gameObject);

            onGrenadeBehaviourComplete();
        }
    }

    public void Setup(GridPosition targetGridPosition, Action onGrenadeBehaviourComplete) {
        this.onGrenadeBehaviourComplete = onGrenadeBehaviourComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
    }
}