using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("In m/s")][SerializeField] float xSpeedFactor = 4f;
    
    float xThrow, xOffset;
    
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        xOffset = xSpeedFactor * Time.deltaTime;
    }
}
