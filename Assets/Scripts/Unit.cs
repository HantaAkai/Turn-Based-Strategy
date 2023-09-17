using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    [SerializeField] private Animator unitAnimator;
    
    private Vector3 targetPosition;

    private void Update() {
        float stoppingDistance = .1f;

        if (Vector3.Distance(targetPosition, this.transform.position) > stoppingDistance) {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            unitAnimator.SetBool("IsWalking", true);
        } else {
            unitAnimator.SetBool("IsWalking", false);
        }


        if (Input.GetMouseButtonDown(0)) {
            Move(MouseWorld.GetPosition());
        }
    }

    private void Move(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }
}