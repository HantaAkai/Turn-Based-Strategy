using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    private Vector3 targetPosition;
    private float moveSpeed = 4f;

    private void Update() {

        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * Time.deltaTime * moveSpeed;

        }

        if (Input.GetKeyDown(KeyCode.T)) {
            Move(new Vector3(4f, 0 , 4f));
        }
    }

    private void Move(Vector3 targetPosition) {
        this.targetPosition = targetPosition;

    }
}