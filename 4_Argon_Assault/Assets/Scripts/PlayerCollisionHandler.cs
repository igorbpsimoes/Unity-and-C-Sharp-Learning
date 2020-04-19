using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour {

    [Tooltip("In seconds")][SerializeField] float sceneLoadDelay = 1.5f;
    [Tooltip("FX prefab on player")][SerializeField] GameObject deathFX = null;

    public delegate void PlayerDeathDelegate();
    public PlayerDeathDelegate playerDeathEvent;

    void OnTriggerEnter(Collider other) {
        StartDeathSequence();
        Invoke(nameof(ReloadScene), sceneLoadDelay);
    }

    private void StartDeathSequence() {
        playerDeathEvent();
        deathFX.SetActive(true);
    }

    private void ReloadScene() { //String referenced
        SceneManager.LoadScene(1);
    }
}
