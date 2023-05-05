using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using System;
using Random = UnityEngine.Random;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class ObjectPool : NinjaMonoBehaviour {
    [SerializeField] private Vector3 minSpawnPosition;
    [SerializeField] private Vector3 maxSpawnPosition;
    [SerializeField] private uint initPoolSize;
    [SerializeField] private AssetReferenceGameObject objectToPool;
    public System.Action OnPoolClear;
    private Stack<PooledObject> stack;
    private PooledObject loadedPooledObject;

    private void Awake() {
        SetupPool();
    }
    private void SetupPool() {
        var logId = "SetupPool";
        stack = new Stack<PooledObject>();
        LoadPooledObject();
    }
    private void LoadPooledObject() {
        AsyncOperationHandle<GameObject> asyncOperationHandle = objectToPool.LoadAssetAsync<GameObject>();
        asyncOperationHandle.Completed += OnObjectLoaded;
    }
    private void OnObjectLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle) {
        var logId = "OnObjectLoaded";
        logd(logId, "AsyncOperationHandle="+asyncOperationHandle.logf());
        if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
            loadedPooledObject = asyncOperationHandle.Result?.GetComponent<PooledObject>();
            if(loadedPooledObject==null) {
                logw(logId, "LoadedObject="+loadedPooledObject.logf()+" => no-op");
                return;
            }
            logd(logId, "Loaded LoadedObject="+loadedPooledObject.logf()+" => populating pool");
            for (int i = 0; i < initPoolSize; i++) {
                var pooledObject = Instantiate(loadedPooledObject).GetComponent<PooledObject>();
                pooledObject.Pool = this;
                pooledObject.gameObject.SetActive(false);
                stack.Push(pooledObject);       
            }
        }
    }
    public void Clear() {
        OnPoolClear?.Invoke();
    }

    public PooledObject GetPooledObject() {
        var logId = "GetPooledObject";
        if(stack.Count==0) {
            if(loadedPooledObject==null) {
                logw(logId, "LoadedPooledObject="+loadedPooledObject.logf()+" => returning null");
                return null;                
            }
            var pooledObject = Instantiate(loadedPooledObject).GetComponent<PooledObject>();
            pooledObject.Pool = this;
            SpawnObject(pooledObject);
            logd(logId, "No objects of type "+pooledObject.GetType()+" in stack. Instantiated new object="+pooledObject.logf()+" for pool="+name+" => returning new object");
            return pooledObject;
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
