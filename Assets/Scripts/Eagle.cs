using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Eagle : NinjaMonoBehaviour, IHittable, ICollectable {
    [SerializeField] private Mover mover;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject visu;
    [SerializeField] private ParticleSystem feathersFx;
    [SerializeField] private float hitDamage = 5f;
    public float MovementSpeed {get; private set;}
    public void OnCollected(PlayerController playerController) {
        string logId = "OnCollected";
        if(playerController==null) {
            logw(logId, "PlayerController is null => no-op");
            return;
        }
        playerController.Engine.AddWearOff(hitDamage);
    }

    public void OnHit() {
        var logId = "OnTriggerEnter";
        logd(logId, "Playing FeatherFX and deactivating Eagle");
        feathersFx.Play();
        visu.SetActive(false);
    }
    private void OnAwake() {
        if(mover==null) {
            mover = GetComponent<Mover>();
        }
    }
    private void OnEnable() {
        Initialize();
    }
    private void Initialize() {
        var logId = "Initialize";
        if(mover==null) {
            logw(logId, "No mover found => no-op");
            return;
        }
        visu.SetActive(true);
        var moverSpeed = mover.Speed; 
        MovementSpeed = moverSpeed;
        animator.speed = moverSpeed;
    }
}
