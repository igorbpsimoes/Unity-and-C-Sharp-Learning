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

    Rigidbody rigidBody;
    AudioSource audioSource;
    
    //Player states
    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

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
        if (state == State.Alive) {
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
            audioSource.Stop();
            rocketJet.Stop();
        }
    }

    private void ApplyThrust() {
        rigidBody.AddRelativeForce(Vector3.up * rcsThrust * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        rocketJet.Play();
    }

    private void HandleRotationInput() {
        FreezeRotation(true); //Takes manual control of rotation

        float frameRotation = rcsRotation * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * frameRotation);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back * frameRotation);
        }

        FreezeRotation(false); //Resumes physical control of rotation
    }

    private void FreezeRotation(bool toFreeze) {
        rigidBody.freezeRotation = toFreeze;
    }

    void OnCollisionEnter(Collision collision) {
        if(state != State.Alive || collisionDisabled) { return; } // ignore all collisions

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
        print("You finished the level!");
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        rocketJet.Stop();
        finish.Play();
        state = State.Transcending;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void LoadNextLevel() {
        ++sceneIndex;
        SceneManager.LoadScene(sceneIndex%SceneManager.sceneCountInBuildSettings);
    }

    private void HandleDeath() {
        print("Dead.");
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        rocketJet.Stop();
        explosion.Play();
        state = State.Dying;
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void LoadFirstLevel() {
        sceneIndex = 0;
        SceneManager.LoadScene(sceneIndex);
    }

    private void ShowWinScreen() {
        print("You Won!");
    }
}
