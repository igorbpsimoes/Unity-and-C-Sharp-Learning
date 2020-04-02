using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour
{
    //rcs = reaction control system
    [SerializeField] float rcsRotation = 120f;
    [SerializeField] float rcsThrust = 30f;
    
    //Player states
    enum State { Alive, Dying, Transcending};
    State state = State.Alive;

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
        if (state == State.Alive) {
            Thrust();
            Rotate();
        } else {
            audioSource.Stop();
        }
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

    private void FreezeRotation(bool toFreeze) {
        rigidBody.freezeRotation = toFreeze;
    }

    void OnCollisionEnter(Collision collision) {
        if(state != State.Alive) { return; } // ignore all collisions

        switch(collision.gameObject.tag) {
            case "Friendly":
                print("Ok!");
                //Do nothing;
                break;
            case "Fuel":
                print("Fuel Replenished.");
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f);
                print("You finished the level!");
                break;
            default:
                print("Wasted.");
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene(1);
    }

    private void ShowWinScreen() {
        print("You Won!");
    }
}
