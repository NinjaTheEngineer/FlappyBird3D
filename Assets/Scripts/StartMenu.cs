using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMenu : MenuController {
    public GameObject highscore;
    public TextMeshProUGUI highscoreText;
    private void Start() {
        var logId = "Start";
        GameManager.OnGameInitialized += Activate;
        activateAnimation = ActivateAnimation;
        deactivateAnimation = DeactivateAnimation;
        var highscoreValue = PlayerPrefs.GetFloat(PlayerPrefs.Key.Highscore);
        highscoreText.text = highscoreValue.ToString("F0");
        highscore.SetActive(highscoreValue>0);
    }
    public void StartGame() {
        var logId = "StartGame";
        Deactivate();
        AudioManager.Instance.PlayUISound(UISounds.BUTTON_CLICK);
        GameManager.Instance.StartGame();
    }
    private void OnDestroy() {
        GameManager.OnGameInitialized -= Activate;
    }
}
