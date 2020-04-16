using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour {

    void OnParticleCollision(GameObject other) {
        transform.parent.gameObject.SendMessage("TakeDamage");
    }
}
