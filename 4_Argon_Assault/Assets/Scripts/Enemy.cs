using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Tooltip("Groups the FX instances for tidiness")][SerializeField] Transform hierarchyGrouper;
    [SerializeField] GameObject deathFX;
    [SerializeField] int scoreOnDeath = 10;

    ScoreBoard scoreboard;

    void Start() {
        scoreboard = FindObjectOfType<ScoreBoard>();
    }

    // TODO: When health system is implemented care about multiple score hits
    void TakeDamage() { //String referenced in ParticleCollisionHandler
        scoreboard.ScoreHit(scoreOnDeath);
        Instantiate(deathFX, transform.position, Quaternion.identity, hierarchyGrouper);
        Destroy(gameObject);
    }
}
