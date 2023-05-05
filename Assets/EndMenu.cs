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
        GameManager.OnGameEnd += Initialize;
        activateAnimation = ActivateAnimation;
        deactivateAnimation = DeactivateAnimation;
        gameObject.SetActive(false);
    }
    public void RestartGame() {
        var logId = "RestartGame";
        Deactivate();
        GameManager.Instance.RestartGame();
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
    protected void ActivateAnimation(float t) {
        transform.localPosition = Vector3.Lerp(deactivatedPosition, activePosition, t);
    }
    protected void DeactivateAnimation(float t) {
        transform.localPosition = Vector3.Lerp(activePosition, deactivatedPosition, t);
    }
    private void OnDestroy() {
        GameManager.OnGameEnd -= Initialize;
    }
}
