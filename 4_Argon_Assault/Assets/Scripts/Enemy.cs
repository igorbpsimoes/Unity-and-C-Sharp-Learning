using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Tooltip("Groups the FX instances for tidiness")][SerializeField] Transform hierarchyGrouper = null;
    [SerializeField] GameObject deathFX = null;
    [SerializeField] int scoreOnDeath = 10;
    [Tooltip("How many hits an enemy can take")][SerializeField] int hitpoints = 10;

    ScoreBoard scoreboard;

    void Awake() {

    }

    void Start() {
        //foreach(GameObject a in FindObjectsOfType<ParticleCollisionHandler>())
        scoreboard = FindObjectOfType<ScoreBoard>();
    }

    void TakeDamage() {
        --hitpoints;
        if(hitpoints < 1) {
            scoreboard.ScoreHit(scoreOnDeath);

            /*By passing the hierarchyGrouper as the parents transform parameter
              we can instantiate as a child and colapse the empty gameobject that
              groups all the deathFX instances on the Hierarchy*/
            Instantiate(deathFX, transform.position, Quaternion.identity, hierarchyGrouper);
            Destroy(gameObject);
        }
    }
}
