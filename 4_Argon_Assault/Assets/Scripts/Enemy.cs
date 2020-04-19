using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Tooltip("Empty GameObject which groups the FX instances for tidiness")] [SerializeField] Transform hierarchyGrouper = null;
    [SerializeField] GameObject deathFX = null;
    [Tooltip("How much the player earns when killing an enemy")][SerializeField] int scoreOnKill = 10;
    [Tooltip("How many hits an enemy can take")] [SerializeField] int hitpoints = 10;

    delegate void OnEnemyKillDelegate(int scoreOnEnemyKill);
    OnEnemyKillDelegate updateScore;

    void Start() {
        //Subscribe to collider delegates
        foreach(EnemyHitHandler collider in GetComponentsInChildren<EnemyHitHandler>()) {
            collider.enemyHitEvent += OnEnemyHit;
        }
        /*Subscribe public method to delegate,
          this way we won't instantiate the scoreboard or find it during runtime*/
        updateScore = FindObjectOfType<ScoreBoard>().ScoreHit;
    }

    void OnEnemyHit() {
        --hitpoints;
        if(hitpoints < 1) {
            OnEnemyDeath();
        }
    }

    void OnEnemyDeath() {
        updateScore(scoreOnKill);

        //Unsubscribe from collider delegates
        foreach (EnemyHitHandler collider in GetComponentsInChildren<EnemyHitHandler>()) {
            collider.enemyHitEvent -= OnEnemyHit;
        }
        /*By passing the hierarchyGrouper as the parents transform parameter
          we can instantiate as a child and colapse the empty gameobject that
          groups all the deathFX instances on the Hierarchy*/
        Instantiate(deathFX, transform.position, Quaternion.identity, hierarchyGrouper);
        Destroy(gameObject);
    }
}
