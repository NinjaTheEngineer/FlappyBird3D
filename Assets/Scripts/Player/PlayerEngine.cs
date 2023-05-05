using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class PlayerEngine : NinjaMonoBehaviour {
    [field: SerializeField] public float MaxDurability {get; private set;}
    private float _durability;
    public float Durability { 
        get => _durability;
        private set {
            var logId = "Durability_set";
            value = Mathf.Clamp(value, 0, MaxDurability);
            logd(logId, "Setting Durability from "+_durability+" to "+ value);
            _durability = value;
        }
    }
    [field: SerializeField] private float WearOffDelay {get; set;} 
    [field: SerializeField] private float WearOffBaseRate {get; set;} 
    public float DurabilityRatio => Durability/MaxDurability;
    public bool IsRunning { get; private set; }
    public void StartEngine() {
        var logId = "StartEngine";
        Durability = MaxDurability;
        IsRunning = true;
        StartCoroutine(WearOffEngineRoutine());
        StartCoroutine(CheckEngineRoutine());
    }
    public void StopEngine() {
        IsRunning = false;
    }
    private IEnumerator CheckEngineRoutine() {
        var logId = "CheckEngineRoutine";
        while(gameObject.activeInHierarchy) {
            if(Durability <= 0) {
                logd(logId, "Durability="+Durability+" => Engine stopped running.");
                Durability = 0;
                IsRunning = false;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    private IEnumerator WearOffEngineRoutine() {
        var logId = "WearOffEngineRoutine";
        while(IsRunning) {
            Durability -= WearOffBaseRate;
            yield return new WaitForSeconds(WearOffDelay);
        }
    }
    public void AddWearOff(float wearAmount) => Durability -= wearAmount;
    public void FixDurability(float fixAmount) {
        Durability += fixAmount;
        IsRunning = true;
        AudioManager.Instance.PlayPlayerSound(PlayerSounds.FIX);
    }
}
