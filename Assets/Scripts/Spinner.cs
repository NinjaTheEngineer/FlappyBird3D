using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Spinner : NinjaMonoBehaviour {
    [SerializeField] private Vector3 spinAxis = Vector3.up;
    [SerializeField] private float spinSpeed = 100f;
    private Quaternion targetRotation;
    private void Awake() {
        targetRotation = transform.rotation;
    }

    private void Update() {
        Quaternion deltaRotation = Quaternion.AngleAxis(spinSpeed * Time.deltaTime, spinAxis);
        targetRotation *= deltaRotation;

        transform.rotation = targetRotation;
    }
}