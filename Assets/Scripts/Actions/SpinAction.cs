using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction {

    public delegate void SpinCompleteDelegate();

    private float spinProgress;
    private SpinCompleteDelegate onSpinComplete;

    private void Update() {
        if (!isActive) {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        spinProgress += spinAddAmount;

        if (spinProgress >= 360) {
            isActive = false;
            onSpinComplete();
        }
    }


    public void Spin(SpinCompleteDelegate onSpinComplete) {
        this.onSpinComplete = onSpinComplete;
        isActive = true;
        spinProgress = 0;
    }
}