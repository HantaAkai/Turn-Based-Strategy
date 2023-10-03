using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour {

    public static TurnSystem Instance;

    public event EventHandler OnTurnChanged;

    private int turnNumber = 1;
    private bool isPlayerTurn = true;
    public bool IsPlayerTurn {get {return isPlayerTurn;}}
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
        if (!isPlayerTurn) {
            turnNumber++;
        }

        isPlayerTurn = !isPlayerTurn;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

}