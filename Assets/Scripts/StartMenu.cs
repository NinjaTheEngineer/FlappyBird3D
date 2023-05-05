using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MenuController {
    private void Start() {
        var logId = "Start";
        GameManager.OnRestartGame += Activate;
        activateAnimation = ActivateAnimation;
        deactivateAnimation = DeactivateAnimation;
    }
    public void StartGame() {
        var logId = "StartGame";
        Deactivate();
        GameManager.Instance.StartGame();
    }
    protected void ActivateAnimation(float t) {
        transform.localPosition = Vector3.Lerp(deactivatedPosition, activePosition, t);
    }
    protected void DeactivateAnimation(float t) {
        transform.localPosition = Vector3.Lerp(activePosition, deactivatedPosition, t);
    }
    private void OnDestroy() {
        GameManager.OnRestartGame -= Activate;
    }
}
