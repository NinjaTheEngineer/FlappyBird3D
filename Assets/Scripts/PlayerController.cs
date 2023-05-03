using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PlayerController : NinjaMonoBehaviour {
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxUpRotation = 60f;
    [SerializeField] private float maxDownRotation = -60f;
    [SerializeField] private float upRotationMultiplier = 2.5f;
    [SerializeField] private float downRotationMultiplier = 1f;   
    [SerializeField] private float fallDelay = 0.25f;
    [SerializeField] private Rigidbody rb;
    private bool isFlyingUp = false;
    private float timeSinceLastImpulse = 0;
    private Quaternion targetRotation;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        var logId = "Update";
        if(rb==null) {
            logw(logId, "Rigidbody not found => no-op", true);
            return;
        }
        if (Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector3(0f, jumpForce, 0f);
            isFlyingUp = true;
            timeSinceLastImpulse = 0;
        }
        if(isFlyingUp){
            timeSinceLastImpulse += Time.deltaTime;
        }
        if(timeSinceLastImpulse>=fallDelay) {
            isFlyingUp = false;
        }
        var currentEulerAngles = transform.rotation.eulerAngles;
        targetRotation = Quaternion.Euler(isFlyingUp?maxUpRotation:maxDownRotation, currentEulerAngles.y, currentEulerAngles.z);
    }
    private void FixedUpdate() {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * (isFlyingUp ? upRotationMultiplier : downRotationMultiplier));
    }
}