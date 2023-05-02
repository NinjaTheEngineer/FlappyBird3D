using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PooledObject : NinjaMonoBehaviour {
    public ObjectPool Pool;
    public void ReturnToPool() {
        string logId = "ReturnToPool";
        if(Pool==null) {
            logw(logId, "Pool="+Pool.logf()+" => Destroying "+gameObject.logf());
            Destroy(gameObject);
            return;
        }
        logd(logId, "Returning "+gameObject.logf()+" to pool="+Pool.logf());
        Pool.ReturnToPool(this);        
    }
}
