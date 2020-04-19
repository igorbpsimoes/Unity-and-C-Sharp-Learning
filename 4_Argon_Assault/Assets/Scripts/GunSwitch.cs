using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitch : MonoBehaviour {
    
    ParticleSystem.EmissionModule emission;

    void Start() {
        emission = GetComponent<ParticleSystem>().emission;
        FindObjectOfType<PlayerController>().isShootingEvent += SetGunActive;
    }

    void SetGunActive(bool isActive) {
        emission.enabled = isActive;
    }
}
