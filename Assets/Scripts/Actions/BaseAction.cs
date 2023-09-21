using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour {

    protected bool isActive;
    protected Unit unit;

    protected virtual void Awake() {
        unit = GetComponent<Unit>();
    }

}