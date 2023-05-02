using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Eagle : NinjaMonoBehaviour, IHittable {
    [SerializeField] private Mover mover;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject visu;
    [SerializeField] private ParticleSystem feathersFx;
    public float MovementSpeed {get; private set;}
    public void OnTriggerEnter() {
        string logId = "OnTriggerEnter";
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
        string logId = "OnEnable";
        if(mover==null) {
            logw(logId, "Eagle="+gameObject.logf()+" doesn't have a Mover component => returning");
            return;
        }
        visu.SetActive(true);
        var moverSpeed = mover.Speed; 
        MovementSpeed = moverSpeed;
        animator.speed = moverSpeed;
    }
    
}
