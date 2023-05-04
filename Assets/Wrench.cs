using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Wrench : PooledObject, IOutOfBoundsable, ICollectable {
    [SerializeField] private float fixAmount;
    public void OnCollected(PlayerController playerController) {
        string logId = "OnCollected";
        if(playerController==null) {
            logw(logId, "PlayerController is null => no-op");
            return;
        }
        playerController.Engine.FixDurability(fixAmount);
        ReturnToPool();
    }

    public void OnOutOfBounds() {
        ReturnToPool();
    }
}
