using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndMenu : MenuController {
    public GameObject scoreVisu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    private void Start() {
        var logId = "Start";
        activateAnimation = ActivateAnimation;
        deactivateAnimation = DeactivateAnimation;
        GameManager.OnGameEnd += Initialize;
        gameObject.SetActive(false);
    }
    public void RestartGame() {
        var logId = "RestartGame";
        Deactivate();
        GameManager.Instance.InitializeGame();
    }
    public void Initialize() {
        var gameManager = GameManager.Instance;
        var score = gameManager.Score;
        var highscore = gameManager.Highscore;
        scoreText.text = score.ToString("F0");
        highscoreText.text = highscore.ToString("F0");
        scoreVisu.SetActive(score!=highscore);
        Activate();
    }
    private void OnDestroy() {
        GameManager.OnGameEnd -= Initialize;
    }
}
