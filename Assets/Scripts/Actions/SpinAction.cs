using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction {

    private float spinProgress;

    private void Update() {
        if (!isActive) {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        spinProgress += spinAddAmount;

        if (spinProgress >= 360) {
            isActive = false;
        }
    }


    public void Spin() {
        isActive = true;
        spinProgress = 0;
    }
}