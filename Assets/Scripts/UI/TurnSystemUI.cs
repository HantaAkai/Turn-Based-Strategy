using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour {

    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private GameObject enemyTurnVisualGameObject;

    private void Start() {
        endTurnButton.onClick.AddListener(() => { 
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e) {
        UpdateTurnText();
        UpdateEnemyTurnVisual(); 
        UpdateEndTurnButtonVisibility();
    }

    private void UpdateTurnText() {
        int turnNumber = TurnSystem.Instance.TurnNumber;
        turnNumberText.text = $"TURN {turnNumber}";
    }

    private void UpdateEnemyTurnVisual() {
        enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn);
    }

    private void UpdateEndTurnButtonVisibility() {
        endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn);
    }

}