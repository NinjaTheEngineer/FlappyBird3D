using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Eagle : NinjaMonoBehaviour, IOutOfBoundsable {
    [SerializeField] private Mover mover;
    [SerializeField] private Animator animator;
    public float MovementSpeed {get; private set;}
    public void OnOutOfBounds() {
        string logId = "OnOutOfBounds";
        logd(logId, name+" is Out of bounds!", true);
    }

    private void OnEnable() {
        var moverSpeed = mover.Speed; 
        MovementSpeed = moverSpeed;
        animator.speed = moverSpeed;
    }
    
}
