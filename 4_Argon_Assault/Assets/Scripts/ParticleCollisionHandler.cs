using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour {

    public delegate void EnemyParticleCollisionDelegate();
    public event EnemyParticleCollisionDelegate enemyParticleCollisionEvent;

    void OnParticleCollision(GameObject other) {
        //Look Null-conditional operator
        //enemyParticleCollisionEvent?.Invoke();
        transform.parent.transform.parent.gameObject.SendMessage("TakeDamage");
    }
}
