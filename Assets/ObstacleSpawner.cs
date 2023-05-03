using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class ObstacleSpawner : NinjaMonoBehaviour {
    public List<ObjectPool> obstaclePools;
    private void Start() {
        StartCoroutine(SpawnObstacleRoutine());
    }

    private IEnumerator SpawnObstacleRoutine() {
        string logId = "SpawnObstacleRoutine";
        var obstaclePoolsCount = obstaclePools.Count;
        if(obstaclePoolsCount==0) {
            loge(logId, "No pools found => no-op");
            yield break;
        }
        while(true) {
            var randomPool = obstaclePools[Random.Range(0, obstaclePoolsCount)];
            if(randomPool==null) {
                logw(logId, "RandomPool="+randomPool.logf()+" => continuing");
                continue;
            }
            randomPool.GetPooledObject();
            yield return new WaitForSeconds(Random.Range(0.25f, 1.25f));
        }
    }
    
}
