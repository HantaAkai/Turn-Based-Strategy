using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour {

    public static TurnSystem Instance;

    public event EventHandler OnTurnChanged;

    private int turnNumber = 1;
    public int TurnNumber { get { return turnNumber; } }

    private void Awake() {

        if (Instance != null) {
            Debug.LogError("There is more than one TurnSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void NextTurn() {
        turnNumber++;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

}