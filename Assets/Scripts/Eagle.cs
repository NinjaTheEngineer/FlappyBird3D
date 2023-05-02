using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Eagle : NinjaMonoBehaviour {
    [SerializeField] private Mover mover;
    [SerializeField] private Animator animator;
    public float MovementSpeed {get; private set;}

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
        var moverSpeed = mover.Speed; 
        MovementSpeed = moverSpeed;
        animator.speed = moverSpeed;
    }
    
}
