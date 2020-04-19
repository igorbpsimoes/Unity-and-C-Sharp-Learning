using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    
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
