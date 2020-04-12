using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Speed factor
    [Tooltip("In m/s")][SerializeField] float speed = 20f;
    //Screen limits
    [Tooltip("In m")][SerializeField] float xMax = 15f;
    [Tooltip("In m")] [SerializeField] float xMin = -15f;
    [Tooltip("In m")] [SerializeField] float yMax = 5f;
    [Tooltip("In m")] [SerializeField] float yMin = -10f;

    //Rotation Order = Y > X > Z; Yaw > Pitch > Roll
    //Rotation due to vertical translation
    [SerializeField] float positionPitchFactor = -1f;
    [SerializeField] float controlPitchFactor = -15f;
    //Rotation due to horizontal translation
    [SerializeField] float positionYawFactor = 1f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
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
        
        float xOffset = speed * xThrow * Time.deltaTime;
        float yOffset = speed * yThrow * Time.deltaTime;
        
        float xRawPos = transform.localPosition.x + xOffset;
        float yRawPos = transform.localPosition.y + yOffset;
        
        //Limits spaceship position to fit inside screen
        float xClampedPos = Mathf.Clamp(xRawPos, xMin, xMax);
        float yClampedPos = Mathf.Clamp(yRawPos, yMin, yMax);

        transform.localPosition = new Vector3(xClampedPos, yClampedPos, transform.localPosition.z);
    }

    private void ProcessRotation() {
        float pitchDueToPosition = (transform.localPosition.y + 5) * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
