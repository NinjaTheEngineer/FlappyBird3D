using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class GameManager : NinjaMonoBehaviour {
    [SerializeField] private PlayerController playerPrefab;
    public PlayerController Player { get; private set;}
    public enum GameState {
        None,
        Started,
        Paused,
        Ended
    }
    public GameState CurrentState {get; private set;}
    public static GameManager Instance;
    [SerializeField] private float _gameSpeed;
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
    public static System.Action OnGameStart;
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
        Player = Instantiate(playerPrefab);
    }
    public void StartGame() {
        var logId = "StartGame";
        if(!Player || !Player.isActiveAndEnabled) {
            Player = Instantiate(playerPrefab);
        }
        GameSpeed = 2f;
        StartCoroutine(IncreaseGameSpeedRoutine());
        CurrentState = GameState.Started;
        OnGameStart?.Invoke();
    }
    public void PlayerDestroyed() {
        var logId = "PlayerDestroyed";
        logd(logId, "Player destroyed!");
        CurrentState = GameState.Ended;
        GameSpeed = 0;
        OnGameEnd?.Invoke();
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
