using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Mover : NinjaMonoBehaviour {
    [SerializeField] private Vector3 moveAxis = Vector3.right;
    [field: SerializeField] public float Speed {get; private set;}
    private void Update() {
        transform.position -= Time.deltaTime * moveAxis * Speed;
    }
}
