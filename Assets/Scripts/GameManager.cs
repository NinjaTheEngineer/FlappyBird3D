using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class GameManager : NinjaMonoBehaviour {
    public enum GameState {
        Start,
        Started,
        Paused,
        Ended
    }
    public GameState CurrentState {get; private set;}
    public static GameManager Instance;
    [SerializeField] private float initialGameSpeed;
    private float _gameSpeed;
    public float GameSpeed {
        get => _gameSpeed;
        private set {
            var logId = "GameSpeed_set";
            if(value<0) {
                value = 0;
            }
            logd(logId, "Setting speed from "+_gameSpeed+" to "+value);
            _gameSpeed = value;
            OnGameSpeedChange?.Invoke(_gameSpeed);
        }
    }
    [SerializeField] [Range(1, 20)] private float speedIncreaseDelay;
    [SerializeField] [Range(0,1)] private float speedIncreaseDelayRate = 0.95f; 
    [SerializeField] [Range(1, 2)] private float speedIncreaseRate = 1.1f; 
    [SerializeField] private PlayerController playerPrefab;
    public PlayerController Player { get; private set;}
    private float _distanceTravelled;
    private float hitScore;
    public float Score => hitScore + (_distanceTravelled*0.1f);
    public float Highscore { get; private set; }
    public void AddHitScore(float scoreAmount) => hitScore+=scoreAmount;
    public static System.Action OnGameInitialized;
    public static System.Action OnGameStarted;
    public static System.Action OnGameEnd;
    public static System.Action<float> OnGameSpeedChange;
    private void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if(Instance==null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        Highscore = PlayerPrefs.GetFloat(PlayerPrefs.Key.Highscore);
        InitializeGame();
    }
    public void StartGame() {
        var logId = "StartGame";
        if(!Player || !Player.isActiveAndEnabled) {
            logd(logId, "Player not initialized => Initializing Player");
            Player = Instantiate(playerPrefab);
        }
        _distanceTravelled = 0;
        GameSpeed = initialGameSpeed;
        hitScore = 0;
        CurrentState = GameState.Started;
        StartCoroutine(IncreaseGameSpeedRoutine());
        StartCoroutine(TimeInGameRoutine());
        OnGameStarted?.Invoke();
    }
    public void PlayerDestroyed() {
        var logId = "PlayerDestroyed";
        if(CurrentState==GameState.Ended) {
            logw(logId, "Game already over => no-op");
            return;
        }
        logd(logId, "Player destroyed!");
        CurrentState = GameState.Ended;
        GameSpeed = 0;
        GameOver();
    }
    public void GameOver() {
        var logId = "GameOver";
        CurrentState = GameState.Ended;
        logd(logId, " => Invoking OnGameEnd");
        var highscore = Highscore;
        var score = Score;
        if(score>highscore) {
            PlayerPrefs.SetFloat(PlayerPrefs.Key.Highscore, score);
            Highscore = score;
        }
        OnGameEnd?.Invoke();
    }
    public void InitializeGame() {
        var logId = "InitializeGame";
        logd(logId, "Initializing game");
        CurrentState = GameState.Start;
        OnGameInitialized?.Invoke();
        GameSpeed = initialGameSpeed;
        Player = Player==null?Instantiate(playerPrefab):Player;
        Player?.Initialize();
    }
    public float timeUpdateDelay = 0.5f;
    public IEnumerator TimeInGameRoutine() {
        var logId = "TimeInGameRoutine";
        while(CurrentState==GameState.Started) {
            _distanceTravelled += (timeUpdateDelay*_gameSpeed);
            yield return new WaitForSeconds(0.05f);
        }
    }
    public IEnumerator IncreaseGameSpeedRoutine() {
        var logId = "IncreaseGameSpeedRoutine";
        while(CurrentState==GameState.Started) {
            yield return new WaitForSeconds(speedIncreaseDelay);
            speedIncreaseDelay *= speedIncreaseDelayRate;
            GameSpeed *= speedIncreaseRate;
        }
    }

    public void PauseGame() {
        CurrentState = GameState.Started;
    }
}
