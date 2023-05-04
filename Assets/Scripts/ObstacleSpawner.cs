using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class ObstacleSpawner : NinjaMonoBehaviour {
    public List<ObjectPool> obstaclePools;
    private List<ObjectPool> initializedPools = new List<ObjectPool>();
    [SerializeField] [Range(0.5f, 2f)]private float minSpawnDelay = 1f;
    [SerializeField] [Range(1f, 5f)]private float maxSpawnDelay = 3f;
    private void Start() {
        GameManager.OnGameStart += InitializeSpawner;
    }
    private void InitializeSpawner() {
        ClearObstacles();
        StartCoroutine(SpawnObstacleRoutine());
    }
    private void InitializePools() {
        var logId = "InitializePools";
        var obstaclePoolsCount = obstaclePools.Count;
        if(obstaclePoolsCount==0) {
            loge(logId, "No pools found => no-op");
            return;
        }
        for (int i = 0; i < obstaclePoolsCount; i++) {
            initializedPools.Add(Instantiate(obstaclePools[i]));
        }
    }
    private void ClearObstacles() {
        var logId = "ClearObstacles";
        var initializedPoolsCount = initializedPools.Count;
        if(initializedPoolsCount==0) {
            logd(logId, "No Initialized Pools to clear => returning");
            return;
        }
        for (int i = 0; i < initializedPoolsCount; i++) {
            var currentPool = initializedPools[i];
            currentPool.Clear();
        }
    }
    private IEnumerator SpawnObstacleRoutine() {
        var logId = "SpawnObstacleRoutine";
        var obstaclePoolsCount = obstaclePools.Count;
        if(obstaclePoolsCount==0) {
            loge(logId, "No pools found => no-op");
            yield break;
        }
        var initializedPoolsCount = initializedPools.Count;
        if(initializedPoolsCount==0) {
            logw(logId, "Pools not yet initialized => Initializing pools");
            InitializePools();
            yield return true;
        }
        initializedPoolsCount = initializedPools.Count;
        while(GameManager.Instance.CurrentState==GameManager.GameState.Started) {
            var randomPool = initializedPools[Random.Range(0, initializedPoolsCount)];
            if(randomPool==null) {
                logw(logId, "RandomPool="+randomPool.logf()+" => continuing");
                continue;
            }
            randomPool.GetPooledObject();
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    private void OnDisable() {
        GameManager.OnGameStart -= () => StartCoroutine(SpawnObstacleRoutine());
    }
    
}
