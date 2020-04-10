using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("In m/s")][SerializeField] float xSpeedFactor = 15f;
    [Tooltip("In m")][SerializeField] float xMax = 15f;
    [Tooltip("In m")] [SerializeField] float xMin = -15f;

    [Tooltip("In m/s")] [SerializeField] float ySpeedFactor = 15f;
    [Tooltip("In m")] [SerializeField] float yMax = 5f;
    [Tooltip("In m")] [SerializeField] float yMin = -10f;

    float xThrow, xOffset, xRawPos, xClampedPos;
    float yThrow, yOffset, yRawPos, yClampedPos;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        HandleHorizontalMovement();
        HandleVerticalMovement();
    }

    private void HandleHorizontalMovement() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        xOffset = xSpeedFactor * xThrow * Time.deltaTime;
        xRawPos = transform.localPosition.x + xOffset;
        xClampedPos = Mathf.Clamp(xRawPos, xMin, xMax);
        transform.localPosition = new Vector3(xClampedPos, transform.localPosition.y, transform.localPosition.z);
    }
    private void HandleVerticalMovement() {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        yOffset = ySpeedFactor * yThrow * Time.deltaTime;
        yRawPos = transform.localPosition.y + yOffset;
        yClampedPos = Mathf.Clamp(yRawPos, yMin, yMax);
        transform.localPosition = new Vector3(transform.localPosition.x, yClampedPos, transform.localPosition.z);
    }
}
