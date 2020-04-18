using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : MonoBehaviour {

    public delegate void EnemyHitDelegate();
    public event EnemyHitDelegate enemyHitEvent;

    void OnParticleCollision(GameObject other) {
        //Look Null-conditional operator
        enemyHitEvent?.Invoke();
    }
}
