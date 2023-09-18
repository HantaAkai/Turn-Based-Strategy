using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {
    [SerializeField] private Transform gridDebugObjectPrefab;


    private GridSystem gridSystem;

    private void Start() {
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

        Debug.Log(new GridPosition(5,7));
    }

    private void Update() {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}