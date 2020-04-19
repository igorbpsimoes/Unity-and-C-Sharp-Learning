using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            if (Input.anyKeyDown) {
                Invoke(nameof(LoadFirstLevel), 1f);
            }
        }
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(1);
    }
}
