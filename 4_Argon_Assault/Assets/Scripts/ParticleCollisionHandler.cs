using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour {
    // Start is called before the first frame update
    void OnParticleCollision(GameObject other) {
        transform.parent.gameObject.SendMessage("TakeDamage");
    }
}
