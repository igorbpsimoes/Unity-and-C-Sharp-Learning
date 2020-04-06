using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour
{
    //rcs = reaction control system
    [SerializeField] float rcsRotation = 120f;
    [SerializeField] float rcsThrust = 1000f;
    [SerializeField] float levelLoadDelay = 2.45f;
    //Audio
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;
    //Particles
    [SerializeField] ParticleSystem rocketJet;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem finish;

    //Components
    Rigidbody rigidBody;
    AudioSource audioSource;

    //Control variables
    bool isTranscending = false;
    bool collisionDisabled = false;
    int sceneIndex;

    // Start is called before the first frame update
    void Start() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (!isTranscending) {
            HandleThrustInput();
            HandleRotationInput();
        }
        if (Debug.isDebugBuild) {
            HandleDebugKeys();
        }
    }

    private void HandleDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        } else if(Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void HandleThrustInput() {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        } else {
            StopApplyingThrust();
        }
    }

    private void ApplyThrust() {
        rigidBody.AddRelativeForce(Vector3.up * rcsThrust * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        rocketJet.Play();
    }

    private void StopApplyingThrust() {
        audioSource.Stop();
        rocketJet.Stop();
    }
    private void HandleRotationInput() {
        rigidBody.angularVelocity = Vector3.zero; //Remove rotation due to physics

        float rotationThisFrame = rcsRotation * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(isTranscending || collisionDisabled) { return; } // ignore all collisions

        switch(collision.gameObject.tag) {
            case "Friendly":
                print("Ok!");
                //Do nothing;
                break;
            case "Fuel":
                print("Fuel Replenished.");
                break;
            case "Finish":
                HandleLevelFinish();
                break;
            default:
                HandleDeath();
                break;
        }
    }

    private void HandleLevelFinish() {
        StopApplyingThrust();
        ApplySuccessEffects();
        isTranscending = true;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void ApplySuccessEffects() {
        audioSource.PlayOneShot(success);
        finish.Play();
    }

    private void LoadNextLevel() {
        ++sceneIndex;
        SceneManager.LoadScene(sceneIndex%SceneManager.sceneCountInBuildSettings);
    }

    private void HandleDeath() {
        StopApplyingThrust();
        ApplyDeathEffects();
        isTranscending = true;
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void ApplyDeathEffects() {
        audioSource.PlayOneShot(death);
        explosion.Play();
    }

    private void LoadFirstLevel() {
        sceneIndex = 0;
        SceneManager.LoadScene(sceneIndex);
    }
}
