using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using NinjaTools;

[System.Serializable]
public class AssetReferenceObstacle : AssetReferenceT<Obstacle> {
    public AssetReferenceObstacle(string guid) : base(guid) {}
}

[System.Serializable]
public class AssetReferenceObjectPool : AssetReferenceT<ObjectPool> {
    public AssetReferenceObjectPool(string guid) : base(guid) {}
}

[System.Serializable]
public class AssetReferencePooledObject : AssetReferenceT<PooledObject> {
    public AssetReferencePooledObject(string guid) : base(guid) {}
}

public class SpawnObjectAddressables : NinjaMonoBehaviour {
    [SerializeField] private List<AssetReferenceGameObject> assetReferences;
    private void OnEnable() {
        var logId = "OnEnable";
        AsyncLoadObjects();
    }
    private void AsyncLoadObjects() {
        var logId = "AsyncLoadObjects";
        var assetReferencesCount = assetReferences.Count;
        if(assetReferencesCount==0) {
            logw(logId, "No AssetReferences to load => no-op");
            return;
        }
        for (int i = 0; i < assetReferencesCount; i++) {
            var currentAssetReference = assetReferences[i];
            AsyncOperationHandle<GameObject> asyncOperationHandle = currentAssetReference.LoadAssetAsync<GameObject>();
            asyncOperationHandle.Completed += OnGameObjectLoaded;
        }
    }
    private void OnGameObjectLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle) {
        var logId = "OnGameObjectLoaded";
        if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
            var loadedObject = asyncOperationHandle.Result;
            logd(logId, "Object="+loadedObject.logf()+" Loaded!");
        }
    }
}
