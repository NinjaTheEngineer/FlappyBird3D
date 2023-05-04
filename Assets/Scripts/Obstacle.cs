using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Obstacle : PooledObject, IOutOfBoundsable {
    [SerializeField] private Vector3 minSpawnPosition;
    [SerializeField] private Vector3 maxSpawnPosition;
    
    public void OnEnable() {
        var logId = "OnEnable";
        var spawnPosition = new Vector3(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y),
            Random.Range(minSpawnPosition.z, maxSpawnPosition.z)); 
        logd(logId, "Setting "+gameObject.logf()+" position from "+transform.position+" to "+spawnPosition);
        transform.position = spawnPosition;
    }
    public void OnOutOfBounds() {
        string logId = "OnOutOfBounds";
        logd(logId,"Returning to pool", true);
        ReturnToPool();
    }
}
