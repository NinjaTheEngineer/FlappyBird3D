using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NinjaTools;

public class InGameUI : NinjaMonoBehaviour {
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
}
