using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PlayerController : NinjaMonoBehaviour {
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxJumpHeight = 7f;
    [SerializeField] private float maxUpRotation = 60f;
    [SerializeField] private float maxDownRotation = -60f;
    [SerializeField] private float upRotationMultiplier = 2.5f;
    [SerializeField] private float downRotationMultiplier = 1f;   
    [SerializeField] private float fallDelay = 0.25f;
    [SerializeField] private Rigidbody rb;
    [field: SerializeField] public PlayerEngine Engine {get; private set;}
    private bool isFlyingUp = false;
    private float timeSinceLastImpulse = 0;
    private Quaternion targetRotation;
    private void Awake() {
        GameManager.OnGameStart += ResetPlayer;
        GameManager.OnGameEnd += PlayerDestroyed;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    private void ResetPlayer() {
        IsControllable = true;
        Engine.StartEngine();
    }
    private bool _isControllable;
    public bool IsControllable {
        get => _isControllable;
        private set {
            var logId = "IsControllable_set";
            logd(logId, "Setting IsControllable from "+_isControllable+" to "+value);
            _isControllable = value;
            rb.isKinematic = !_isControllable;
        }
    }
    private void Update() {
        var logId = "Update";
        if(rb==null) {
            logw(logId, "Rigidbody not found => no-op", true);
            return;
        }
        if(!IsControllable || !Engine.EngineRunning) {
            return;
        }
        var jumpInput = Input.GetButtonDown("Jump") || (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began);
        if(jumpInput && transform.position.y <= maxJumpHeight) {
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
        if(!IsControllable) {
            return;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * (isFlyingUp ? upRotationMultiplier : downRotationMultiplier));
    }
    private void OnDestroy() {
        GameManager.OnGameStart -= ResetPlayer;
        GameManager.OnGameEnd -= PlayerDestroyed;
    }
    private void PlayerDestroyed() {
        var logId = "PlayerDestroyed";
        IsControllable = false;
        Destroy(gameObject);
    }
}