using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            if (Input.anyKeyDown) {
                Invoke("LoadFirstLevel", 1f);
            }
        }
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(1);
    }
}
