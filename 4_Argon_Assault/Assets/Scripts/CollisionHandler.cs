using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [Tooltip("In seconds")][SerializeField] float sceneLoadDelay = 1.5f;
    [Tooltip("FX prefab on player")][SerializeField] GameObject deathFX = null;

    void OnTriggerEnter(Collider other) {
        StartDeathSequence();
        Invoke("ReloadScene", sceneLoadDelay);
    }

    private void StartDeathSequence() {
        gameObject.SendMessage("DisableControls");
        deathFX.SetActive(true);
    }

    private void ReloadScene() { //String referenced
        SceneManager.LoadScene(1);
    }
}
