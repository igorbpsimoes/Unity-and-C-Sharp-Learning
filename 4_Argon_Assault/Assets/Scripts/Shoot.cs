using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    ParticleSystem.EmissionModule emission;

    // Start is called before the first frame update
    void Start() {
        emission = GetComponent<ParticleSystem>().emission;
        FindObjectOfType<PlayerController>().isShootingEvent += SetGunActive;
    }

    void SetGunActive(bool isActive) {
        emission.enabled = isActive;
    }
}
