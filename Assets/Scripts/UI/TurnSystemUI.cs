using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour {

    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;

    private void Start() {
        endTurnButton.onClick.AddListener(() => { 
            TurnSystem.Instance.NextTurn();

            UpdateTurnNumberText();
        });

        UpdateTurnNumberText();
    }

    private void UpdateTurnNumberText() {
        int turnNumber = TurnSystem.Instance.TurnNumber;
        turnNumberText.text = $"TURN {turnNumber}";
    }

}