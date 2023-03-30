using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    private const string IS_WALKING = "IsWlaking";

    [SerializeField] private Animator unitAnimator; 

    private Vector3 targetPosition;
    private Vector3 startingLerpPosition;
    private float currentLerpTime = 0;
    private float maxLerpTime = 1;

    private float moveSpeed = 4f;

  
    private void Update() {

        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            //This Lerp can be changed into miss use
            //transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            //TODO: Might want to look at transform.rotation or transform.eulerAngles to make the rotation time fixed
            float rotationSpeed = 2f;
            float rotationPercentage = currentLerpTime / maxLerpTime;
            transform.forward = Vector3.Lerp(startingLerpPosition, moveDirection, rotationPercentage * rotationSpeed);
            currentLerpTime += Time.deltaTime;

            unitAnimator.SetBool(IS_WALKING, true);
        } else {
            unitAnimator.SetBool (IS_WALKING, false);
            startingLerpPosition = transform.forward;
            currentLerpTime = 0;
        }

        if (Input.GetMouseButtonDown(0)) {
            Move(MouseWorld.GetPosition());
            startingLerpPosition = transform.forward;
            currentLerpTime = 0;
        }
    }

    private void Move(Vector3 targetPosition) {
        this.targetPosition = targetPosition;

    }
}