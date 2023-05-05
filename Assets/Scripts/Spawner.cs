using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public abstract class Spawner : NinjaMonoBehaviour {
    public List<ObjectPool> objectPools;
    protected List<ObjectPool> initializedPools = new List<ObjectPool>();
    [Tooltip("If SpawnDelay is 0, SpawnDelay will be a random value inside the range MinSpawnDelay and MaxSpawnDelay.")]
    [SerializeField] [Range(0f, 5f)] protected float spawnDelay = 0;
    [SerializeField] [Range(0f, 2f)] protected float minSpawnDelay = 1f;
    [SerializeField] [Range(0.5f, 5f)] protected float maxSpawnDelay = 3f;
    private void Start() {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameInitialized += ClearObjects;
    }
    public abstract void OnGameStarted();
    public abstract IEnumerator SpawnRoutine();
    protected void InitializePools() {
        var logId = "InitializePools";
        var obstaclePoolsCount = objectPools.Count;
        if(obstaclePoolsCount==0) {
            loge(logId, "No pools found => no-op");
            return;
        }
        for (int i = 0; i < obstaclePoolsCount; i++) {
            initializedPools.Add(Instantiate(objectPools[i]));
        }
    }
    public void ClearObjects() {
        var logId = "ClearObjects";
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
    private void OnDisable() {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameInitialized -= ClearObjects;
    }
}
