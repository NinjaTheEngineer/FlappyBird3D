using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PlayerCollisions : NinjaMonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        var logId = "OnTriggerEnter";
        IHittable hittable = other.GetComponent<IHittable>();
        if(hittable==null) {
            logd(logId,"Collided with "+gameObject.logf()+" that isn't a Hittable object => no-op");
            return;    
        }
        hittable.OnTriggerEnter();
    }
}
