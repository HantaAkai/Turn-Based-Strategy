using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCrate : MonoBehaviour {

    public static event EventHandler OnAveDestroyed;

    [SerializeField] private Transform crateDestroyedPrefab;

    public GridPosition gridPosition { get; private set; }

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    }

    public void TakeDamage() {
        Transform crateDestroyedTransform = Instantiate(crateDestroyedPrefab, transform.position, transform.rotation);

        ApplyExplosionToChildren(crateDestroyedTransform, 150f, transform.position, 10f);

        Destroy(gameObject);

        OnAveDestroyed?.Invoke(this, EventArgs.Empty);
    }

    private void ApplyExplosionToChildren(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange) {
        foreach (Transform child in root) {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidBody)) {
                childRigidBody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
            }

            ApplyExplosionToChildren(child, explosionForce, explosionPosition, explosionRange);
        }
    }
}