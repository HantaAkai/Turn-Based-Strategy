using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour {

    private const string IS_WALKING = "IsWlaking";

    [SerializeField] private Animator unitAnimator;

    private GridPosition gridPosition;

    private Vector3 targetPosition;
    private Vector3 startingLerpPosition;
    private float currentLerpTime = 0;
    private float maxLerpTime = 1;

    private float moveSpeed = 4f;

    private void Awake() {
        targetPosition = transform.position;
    }

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }


    private void Update() {
        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            Lerp(moveDirection);

            unitAnimator.SetBool(IS_WALKING, true);
        } else {
            unitAnimator.SetBool(IS_WALKING, false);
            ResetLerp();
        }


        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition) {
            //Unit Changed gridPostion
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public void Move(Vector3 targetPosition) {
        ResetLerp();
        this.targetPosition = targetPosition;

    }

    private void Lerp(Vector3 endLerpDirection) {
        //This Lerp can be changed into miss use
        //transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        //TODO: Might want to look at transform.rotation or transform.eulerAngles to make the rotation time fixed
        float rotationSpeed = 2f;
        float rotationPercentage = currentLerpTime / maxLerpTime;
        transform.forward = Vector3.Lerp(startingLerpPosition, endLerpDirection, rotationPercentage * rotationSpeed);
        currentLerpTime += Time.deltaTime;
    }

    private void ResetLerp() {
        startingLerpPosition = transform.forward;
        currentLerpTime = 0;
    }
}