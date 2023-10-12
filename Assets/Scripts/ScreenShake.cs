using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake() {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            cinemachineImpulseSource.GenerateImpulse();
        }
    }

}