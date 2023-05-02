using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;

public class Obstacle : NinjaMonoBehaviour, IOutOfBoundsable {
    public void OnOutOfBounds() {
        Destroy(gameObject);
    }
}
