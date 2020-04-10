using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Speed factor
    [Tooltip("In m/s")][SerializeField] float speed = 15f;
    //Screen limits
    [Tooltip("In m")][SerializeField] float xMax = 15f;
    [Tooltip("In m")] [SerializeField] float xMin = -15f;
    [Tooltip("In m")] [SerializeField] float yMax = 5f;
    [Tooltip("In m")] [SerializeField] float yMin = -10f;

    float xThrow, xOffset, xRawPos, xClampedPos;
    float yThrow, yOffset, yRawPos, yClampedPos;

    //Rotation Order = Y > X > Z

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        
        xOffset = speed * xThrow * Time.deltaTime;
        yOffset = speed * yThrow * Time.deltaTime;
        
        xRawPos = transform.localPosition.x + xOffset;
        yRawPos = transform.localPosition.y + yOffset;
        
        //Limits spaceship position to fit inside screen
        xClampedPos = Mathf.Clamp(xRawPos, xMin, xMax);
        yClampedPos = Mathf.Clamp(yRawPos, yMin, yMax);

        transform.localPosition = new Vector3(xClampedPos, yClampedPos, transform.localPosition.z);
    }

    private void ProcessRotation() {
        throw new NotImplementedException();
    }
}
