using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour {

    public static event EventHandler OnAnyGrenadeExploded;

    [SerializeField] private Transform grenadeExplosionVFXPrefab;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private AnimationCurve arcYAnimationCurve;

    private Vector3 targetPosition;
    private float affectedRadius = 4f;
    private Action onGrenadeBehaviourComplete;
    private float totalDistanceToTarget;
    private Vector3 positionXZ;

    private void Update() {
        Vector3 moveDirection = (targetPosition - positionXZ).normalized;

        float moveSpeed = 15f;
        positionXZ += moveDirection * Time.deltaTime * moveSpeed;

        float distance = Vector3.Distance(positionXZ, targetPosition);
        float distanceNormalized = 1 - distance / totalDistanceToTarget;

        float maxHeight = totalDistanceToTarget/ 4f;
        float positionY = arcYAnimationCurve.Evaluate(distanceNormalized) * maxHeight;
        transform.position = new Vector3(positionXZ.x, positionY, positionXZ.z);

        float reachedTargetDistance = .2f;
        if (distance < reachedTargetDistance) {
            Collider[] colliderArray = Physics.OverlapSphere(targetPosition, affectedRadius);

            foreach (Collider collider in colliderArray) {
                if(collider.TryGetComponent<Unit>(out Unit targetUnit)) {
                    targetUnit.TakeDamage(30);
                }

                if (collider.TryGetComponent<DestructibleCrate>(out DestructibleCrate crate)) {
                    crate.TakeDamage();
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

        positionXZ = transform.position;
        positionXZ.y = 0;
        totalDistanceToTarget = Vector3.Distance(positionXZ, targetPosition);
    }
}