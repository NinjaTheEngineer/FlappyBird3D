using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MenuController {
    private void Start() {
        var logId = "Start";
        GameManager.OnGameInitialized += Activate;
        activateAnimation = ActivateAnimation;
        deactivateAnimation = DeactivateAnimation;
    }
    public void StartGame() {
        var logId = "StartGame";
        Deactivate();
        GameManager.Instance.StartGame();
    }
    private void OnDestroy() {
        GameManager.OnGameInitialized -= Activate;
    }
}
