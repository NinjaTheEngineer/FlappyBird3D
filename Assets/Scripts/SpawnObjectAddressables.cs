using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnObjectAddressables : MonoBehaviour {
    [SerializeField] private AssetReferenceGameObject assetReference;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            //assetReference.InstantiateAsync();
            AsyncOperationHandle<GameObject> asyncOperationHandle = assetReference.LoadAssetAsync<GameObject>();
            asyncOperationHandle.Completed += OnGameObjectLoaded;
        }
    }
    private void OnGameObjectLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle) {
        if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
            Instantiate(asyncOperationHandle.Result);
        } else {
            //Failed to load
        }
    }
}
