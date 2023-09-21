using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour {
    public static GridSystemVisual Instance { get; private set; }

    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    private GridSystemVisualSingle[,] gridSystemVisualSingeArray;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one GridSystemVisual! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() {
        gridSystemVisualSingeArray = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++) {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++) {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition),Quaternion.identity);
                gridSystemVisualSingeArray[x,z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    private void Update() {
        UpdateVisual();
    }

    public void HideAllGridPositions() {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++) {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++) {
                gridSystemVisualSingeArray[x,z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList) {
        foreach (GridPosition gridPosition in gridPositionList) {
            gridSystemVisualSingeArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    public void UpdateVisual() {
        HideAllGridPositions();

        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();

        ShowGridPositionList(selectedAction.GetValidActionGridPositionList());
    }
} 