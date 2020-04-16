using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    // Start is called before the first frame update
    void Awake() {
        //Makes sure only one music player exists
        int objectQtt = FindObjectsOfType<MusicPlayer>().Length;
        if (objectQtt > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
