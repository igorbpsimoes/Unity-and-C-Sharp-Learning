using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Waypoints : MonoBehaviour {

    public static Transform[] points;

    void Awake() {
        points = gameObject.GetComponentsInChildren<Transform>();
    }
}
