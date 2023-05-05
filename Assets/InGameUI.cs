using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NinjaTools;
using TMPro;

public class InGameUI : NinjaMonoBehaviour {
    public TextMeshProUGUI scoreText;
    public Image engineBarFill;
    public float updateDelay = 0.25f;
    PlayerController playerController;
    PlayerEngine playerEngine;
    private void Start() {
        GameManager.OnGameStart += Setup;
        GameManager.OnGameEnd += Deactivate;
        gameObject.SetActive(false);
    }
    private void Setup() {
        var logId = "Setup";
        playerController = GameManager.Instance.Player;
        playerEngine = playerController?.Engine;
        if(playerController==null || playerEngine==null) {
            logw(logId, "PlayerController="+playerController.logf()+" PlayerEngine="+playerEngine.logf()+" => no-op");
            return;
        }
        gameObject.SetActive(true);
        StartCoroutine(UpdateEngineBarRoutine());
        StartCoroutine(UpdateScoreTextRoutine());
    }
    private void Deactivate() {
        gameObject.SetActive(false);
    }
    private IEnumerator UpdateEngineBarRoutine() {
        var logId = "UpdateEngineBarRoutine";
        yield return true;
        while(playerController.IsControllable) {
            var fillAmount = playerEngine.DurabilityRatio;
            engineBarFill.fillAmount = fillAmount;
            logd(logId, "Updating EngineBarFill to "+fillAmount);
            yield return new WaitForSeconds(updateDelay);
        }
    }
    private IEnumerator UpdateScoreTextRoutine() {
        var logId = "UpdateScoreTextRoutine";
        var gameManager = GameManager.Instance;
        while(gameManager.CurrentState==GameManager.GameState.Started) {
            var scoreAmount = gameManager.Score;
            scoreText.text = scoreAmount.ToString("F0");
            yield return new WaitForSeconds(updateDelay);
        }
    }
}
