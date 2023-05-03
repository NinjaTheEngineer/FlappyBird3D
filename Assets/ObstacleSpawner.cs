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
        while(true) {
            var randomPool = Random.Range(0, obstaclePoolsCount);
            obstaclePools[randomPool].GetPooledObject();
            yield return new WaitForSeconds(Random.Range(0.25f, 1.25f));
        }
    }
    
}
