using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour {

    private Vector3 targetPosition;
    private float moveSpeed = 15f;

    private void Update() {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float moveSpeed = 15f;
        transform.position += moveDirection * Time.deltaTime * moveSpeed;

        float reachedTargetDistance = .2f;
        if (Vector3.Distance(transform.position, targetPosition) < reachedTargetDistance) {
            Destroy(gameObject);
        }
    }

    public void Setup(GridPosition targetGridPosition) {
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
    }
}