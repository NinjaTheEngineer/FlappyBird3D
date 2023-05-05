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
    private Vector3 startPosition;
    private Quaternion startRotation;
    private void Awake() {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        GameManager.OnGameStarted += Reset;
        GameManager.OnGameEnd += PlayerDestroyed;
    }
    public void Initialize() {
        var logId = "Initialize";
        rb.isKinematic = true;
        logd(logId,"Setting position from "+transform.position+" to "+startPosition);
        transform.position = startPosition;
        transform.rotation = startRotation;
        StartCoroutine(EnterSceneRoutine());
    }
    public float enteringSpeed = 2f;
    private IEnumerator EnterSceneRoutine() {
        var logId = "EnterSceneRoutine";
        Vector3 targetPosition = new Vector3(0, startPosition.y, startPosition.z);
        logd(logId,"Entering scene from "+transform.position+" to "+targetPosition);
        float distance = Vector3.Distance(startPosition, targetPosition);
        float time = 0f;
        while(time<1 && !_isControllable){
            time += Time.deltaTime * enteringSpeed / distance;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }

        transform.position = targetPosition;
    }
    private void Reset() {
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
        if(!IsControllable || !Engine.IsRunning) {
            return;
        }
        var jumpInput = Input.GetButtonDown("Jump") || (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began);
        if(jumpInput && transform.position.y <= maxJumpHeight) {
            FlyUp();
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
    private void FlyUp() {
        AudioManager.Instance.PlayPlayerSound(PlayerSounds.FLY);
        rb.velocity = new Vector3(0f, jumpForce, 0f);
        isFlyingUp = true;
        timeSinceLastImpulse = 0;
    }
    private void FixedUpdate() {
        if(!IsControllable) {
            return;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * (isFlyingUp ? upRotationMultiplier : downRotationMultiplier));
    }
    private void OnDestroy() {
        GameManager.OnGameStarted -= Reset;
        GameManager.OnGameEnd -= PlayerDestroyed;
    }
    private void PlayerDestroyed() {
        var logId = "PlayerDestroyed";
        IsControllable = false;
        Engine.StopEngine();
        StartCoroutine(PlayerCrashRoutine());
    }
    public float crashingTime = 0.5f;
    public float crashSpeed = 10f;
    public Vector3 axis = Vector3.forward;
    private IEnumerator PlayerCrashRoutine() {
        var logId = "PlayerCrashRoutine";
        var crashTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup - crashTime < crashingTime) {
            transform.position += Time.deltaTime * axis * crashSpeed;
            yield return true;
        }
        rb.isKinematic = false;
        AudioManager.Instance.PlayPlayerSound(PlayerSounds.CRASH);
        //Explosion;
    }
}