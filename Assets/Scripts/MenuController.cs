using System.Collections;
using UnityEngine;
using NinjaTools;
using System;
using UnityEngine.UI;

public abstract class MenuController : NinjaMonoBehaviour {
    public Vector3 activePosition;
    public Vector3 deactivatedPosition;
    protected Action<float> activateAnimation = null;
    protected Action<float> deactivateAnimation = null;
    public float animationTime = 0.5f;
    public MenuState CurrentState {
        get; protected set;
    }
    public enum MenuState {
        Activating,
        Active,
        Deactivating,
        Deactivated
    }
    public virtual void Activate() {
        var logId = "Activate";
        logd(logId, "Activating");
        gameObject.SetActive(true);
        if(activateAnimation==null) {
            logt(logId, "No animation for activating Menu="+name+" => Setting currentState to active");
            CurrentState = MenuState.Active;
        } else {
            CurrentState = MenuState.Activating;
            StartCoroutine(Animate(activateAnimation, () => CurrentState = MenuState.Active));
        }
    }

    public virtual void Deactivate() {
        var logId = "Deactivate";
        if(deactivateAnimation==null) {
            logt(logId, "No animation for deactivating Menu="+name+" => Setting currentState to deactivated");
            CurrentState = MenuState.Deactivated;
            gameObject.SetActive(false);
        } else {
            CurrentState = MenuState.Deactivating;
            StartCoroutine(Animate(deactivateAnimation, () => {
                gameObject.SetActive(false);
                CurrentState = MenuState.Deactivated; 
            }));
        }
    }

    private IEnumerator Animate(Action<float> animationFunction, Action callback=null) {
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < startTime+animationTime) {
            float progress = (Time.realtimeSinceStartup-startTime) / animationTime;
            animationFunction(progress);
            yield return true;
        }
        callback?.Invoke();
    }
    protected virtual void ActivateAnimation(float t) {
        transform.localPosition = Vector3.Lerp(deactivatedPosition, activePosition, t);
    }
    protected virtual void DeactivateAnimation(float t) {
        transform.localPosition = Vector3.Lerp(activePosition, deactivatedPosition, t);
    }
}