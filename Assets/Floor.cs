using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Floor : NinjaMonoBehaviour, IOutOfBoundsable {
    [SerializeField] public float xIncrement = 75;
    public void OnOutOfBounds() {
        var currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x + xIncrement, currentPosition.y, currentPosition.z);
    }
}
