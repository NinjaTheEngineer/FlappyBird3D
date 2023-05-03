using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PooledObject : NinjaMonoBehaviour {
    public ObjectPool Pool;
    private void OnEnable() {
        var logId = "OnEnable";
        var children = GetComponentsInChildren<GameObject>();
        var childrenCount = children.Length;
        if(childrenCount==0) {
            return;
        }
        for (int i = 0; i < childrenCount; i++) {
            children[i].SetActive(true);
        }
    }
    public void ReturnToPool() {
        var logId = "ReturnToPool";
        if(Pool==null) {
            logw(logId, "Pool="+Pool.logf()+" => Destroying "+gameObject.logf());
            Destroy(gameObject);
            return;
        }
        logd(logId, "Returning "+gameObject.logf()+" to pool="+Pool.logf());
        Pool.ReturnToPool(this);        
    }
}
