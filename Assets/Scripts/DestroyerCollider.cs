using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class DestroyerCollider : NinjaMonoBehaviour, IHittable {
    public void OnHit() {
        GameManager.Instance.PlayerDestroyed();
    }
    private void OnCollisionEnter(Collision other) {
        logd("CollisionEnter", name + " collided with "+other.gameObject.logf());
        if(other.gameObject.GetComponent<PlayerController>()) {
            GameManager.Instance.PlayerDestroyed();
        }
    }
}
