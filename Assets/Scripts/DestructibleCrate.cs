using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCrate : MonoBehaviour {

    public static event EventHandler OnAveDestroyed;

    public GridPosition gridPosition { get; private set; }

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    }

    public void TakeDamage() {
        Destroy(gameObject);

        OnAveDestroyed?.Invoke(this, EventArgs.Empty);
    }
}