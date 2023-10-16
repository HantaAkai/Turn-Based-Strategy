using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] private bool isOpen;

    private GridPosition gridPosition;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>(); 
    }

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetDoorAtGridPosition(gridPosition, this);

        if (isOpen) {
            OpenDoor();
        } else {
            CloseDoor();
        }
    }

    public void Interact() {
        if (isOpen) {
            CloseDoor();
        } else {
            OpenDoor();
        }
    }

    private void OpenDoor() {
        isOpen = true;
        animator.SetBool("IsOpen", isOpen);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, isOpen);
    }

    private void CloseDoor() {
        isOpen = false;
        animator.SetBool("IsOpen", isOpen);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, isOpen);
    }
}
