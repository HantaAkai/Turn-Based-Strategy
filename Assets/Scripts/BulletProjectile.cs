using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    [SerializeField] private TrailRenderer trailRenderer;

    private Vector3 targetPosition;

    private void Update() {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);

        float moveSpeed = 200f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if (distanceBeforeMoving < distanceAfterMoving) {
            transform.position = targetPosition;

            trailRenderer.transform.SetParent(null);
            
            Destroy(gameObject);
        }
    }

    public void SetUp(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

}