using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PlayerCollisions : NinjaMonoBehaviour {
    [SerializeField] private PlayerController playerController;
    private void Awake() {
        playerController = playerController??GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other) {
        var logId = "OnTriggerEnter";
        IHittable hittable = other.GetComponent<IHittable>();
        ICollectable collectable = other.GetComponent<ICollectable>();
        if(hittable==null && collectable==null) {
            logd(logId,"Collided with "+gameObject.logf()+" that isn't a hitabble nor collectable object => no-op");
            return;
        }
        hittable?.OnHit();
        collectable?.OnCollected(playerController);
    }
}
