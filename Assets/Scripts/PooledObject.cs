using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PooledObject : NinjaMonoBehaviour {
    private ObjectPool _pool;
    public ObjectPool Pool {
        get => _pool;
        set {
            var logId = "Pool";
            if(_pool) {
                _pool.OnPoolClear -= ReturnToPool;
            }
            logd(logId, "Setting Pool from "+_pool.logf()+" to "+value.logf());
            _pool = value;
            if(_pool) {
                _pool.OnPoolClear += ReturnToPool;
            }
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
