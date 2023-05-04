using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class ObstacleSpawner : Spawner {
    public override void OnGameStarted() {
        ClearObjects();
        StartCoroutine(SpawnRoutine());
    }
    public override IEnumerator SpawnRoutine() {
        var logId = "SpawnRoutine";
        var obstaclePoolsCount = objectPools.Count;
        if(obstaclePoolsCount==0) {
            loge(logId, "No pools found => no-op");
            yield break;
        }
        yield return true;
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
            spawnDelay = spawnDelay==0?Random.Range(minSpawnDelay, maxSpawnDelay):spawnDelay;
            randomPool.GetPooledObject();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void OnDisable() {
        GameManager.OnGameStart -= () => StartCoroutine(SpawnRoutine());
    }
    
}
