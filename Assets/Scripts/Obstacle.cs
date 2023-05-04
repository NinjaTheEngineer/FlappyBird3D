using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Obstacle : PooledObject, IOutOfBoundsable {
    public void OnOutOfBounds() {
        string logId = "OnOutOfBounds";
        logd(logId,"Returning to pool", true);
        ReturnToPool();
    }
}
