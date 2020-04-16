using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Tooltip("Groups the FX instances for tidiness")][SerializeField] Transform hierarchyGrouper;
    [SerializeField] GameObject deathFX;

    // TODO: Care about multiple incorrect destructions
    void TakeDamage() { //String referenced in ParticleCollisionHandler
        Instantiate(deathFX, transform.position, Quaternion.identity, hierarchyGrouper);
        Destroy(gameObject);
    }
}
