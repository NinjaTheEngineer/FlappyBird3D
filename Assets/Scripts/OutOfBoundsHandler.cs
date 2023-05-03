using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class OutOfBoundsHandler : NinjaMonoBehaviour {
    [SerializeField] private float xBounds = -20f;
    [SerializeField] private float checkDelay = 0.5f;
    [SerializeField] private IOutOfBoundsable outOfBoundsable;
    private void Awake() {
        outOfBoundsable = outOfBoundsable??GetComponent<IOutOfBoundsable>();
    }
    private void OnEnable() {
        var logId = "OnEnable";
        if(outOfBoundsable==null) {
            logw(logId, "Object="+name+" doesn't contain a IOutOfBoundsable component => no-op");
            return;
        }
        StartCoroutine(CheckOutOfBoundsRoutine());
    }
    private IEnumerator CheckOutOfBoundsRoutine() {
        var logId = "CheckOutOfBoundsRoutine";
        var waitForSeconds = new WaitForSeconds(checkDelay);
        while(gameObject.activeInHierarchy) {
            if(transform.position.x < xBounds) {
                logd(logId, "OutOfBoundsable="+outOfBoundsable.logf()+" => Executing OnOutOfBounds");
                outOfBoundsable.OnOutOfBounds();
            }
            yield return waitForSeconds;
        }
    }
}

