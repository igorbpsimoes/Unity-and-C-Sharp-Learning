using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController: MonoBehaviour {

    [Header("General")]
    //Speed factor
    [Tooltip("In m/s")][SerializeField] float controlSpeed = 20f;

    [Header("Screen Limits")]
    [Tooltip("In m")][SerializeField] float xMax = 15f;
    [Tooltip("In m")] [SerializeField] float xMin = -15f;
    [Tooltip("In m")] [SerializeField] float yMax = 5f;
    [Tooltip("In m")] [SerializeField] float yMin = -10f;

    //Rotations due to vertical and horizontal translation
    [Header("Screen-position Rotation")]
    [SerializeField] float positionPitchFactor = -1f;
    [SerializeField] float positionYawFactor = 1f;

    [Header("Control-throw Rotation")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    bool isControlEnabled = true;
    
    new Rigidbody rigidbody;

    public delegate void ShootDelegate(bool isShooting);
    public event ShootDelegate isShootingEvent;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if(isControlEnabled) {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void DisableControls() { //Called by string reference in CollisionHandler
        isControlEnabled = false;
        rigidbody.isKinematic = false;
    }

    private void ProcessTranslation() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        
        float xOffset = controlSpeed * xThrow * Time.deltaTime;
        float yOffset = controlSpeed * yThrow * Time.deltaTime;
        
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
    
    private void ProcessFiring() {
        if(CrossPlatformInputManager.GetButton("Shoot")) {
            isShootingEvent?.Invoke(true);
        } else {
            isShootingEvent?.Invoke(false);
        }
    }
}
