using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //rcs = reaction control system
    [SerializeField] float rcsRotation = 120f;
    [SerializeField] float rcsThrust = 30f;

    Rigidbody rigidBody;
    AudioSource audioSource;
    //Vector3 thrustForce = new Vector3(0, 2, 0);
    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        Thrust();
        Rotate();
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up * rcsThrust);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        } else {
            audioSource.Stop();
        }
    }

    private void Rotate() {
        FreezeRotation(true); //Takes manual control of rotation

        float frameRotation = rcsRotation * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * frameRotation);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back * frameRotation);
        }

        FreezeRotation(false); //Resumes physical control of rotation
    }

    private void FreezeRotation(Boolean toFreeze) {
        rigidBody.freezeRotation = toFreeze;
    }

    void OnCollisionEnter(Collision collision) {
        switch(collision.gameObject.tag) {
            case "Friendly":
                print("Ok!");
                //Do nothing;
                break;
            case "Fuel":
                print("Fuel Replenished.");
                break;
            case "Finish":
                print("You Win!");
                break;
            default:
                print("Wasted.");
                break;
        }
    }
}
