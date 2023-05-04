using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using System;
using Random = UnityEngine.Random;

public class ObjectPool : NinjaMonoBehaviour {
    [SerializeField] private Vector3 minSpawnPosition;
    [SerializeField] private Vector3 maxSpawnPosition;
    [SerializeField] private uint initPoolSize;
    [SerializeField] private PooledObject objectToPool;
    public System.Action OnPoolClear;
    private Stack<PooledObject> stack;

    private void Start() {
        SetupPool();
    }
    private void SetupPool() {
        var logId = "SetupPool";
        stack = new Stack<PooledObject>();
        PooledObject pooledObject = null;

        for (int i = 0; i < initPoolSize; i++) {
            pooledObject = Instantiate(objectToPool);
            pooledObject.Pool = this;
            pooledObject.gameObject.SetActive(false);
            stack.Push(pooledObject);       
        }
    }
    public void Clear() {
        OnPoolClear?.Invoke();
    }

    public PooledObject GetPooledObject() {
        var logId = "GetPooledObject";
        if(stack.Count==0) {
            PooledObject newObject = Instantiate(objectToPool);
            newObject.Pool = this;
            SpawnObject(newObject);
            logd(logId, "No objects of type "+newObject.GetType()+" in stack. Instantiated new object="+newObject.logf()+" for pool="+name+" => returning new object");
            return newObject;
        }
        PooledObject nextObject = stack.Pop();
        SpawnObject(nextObject);
        logd(logId, "Popped object="+nextObject.logf()+" from stack => returning object");
        return nextObject;
    }

    private void SpawnObject(PooledObject obj) {
        var logId = "SpawnObject";
        var spawnPosition = new Vector3(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y),
            Random.Range(minSpawnPosition.z, maxSpawnPosition.z)); 
        logd(logId, "Setting "+obj.logf()+" position to "+spawnPosition);
        obj.transform.position = spawnPosition;
        obj.gameObject.SetActive(true);
    }
    

    public void ReturnToPool(PooledObject pooledObject) {
        var logId = "ReturnToPool";
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
