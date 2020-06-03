using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyMovement : MonoBehaviour {

    [SerializeField] float speed = 10f;

    Vector3 moveDirection, translation, distanceToWaypoint, offset;
    int currentWaypoint = 1;
    bool applyOffset = false;

    void Start() {
        GetNextDirection();
    }

    void Update() {
        translation = moveDirection * speed * Time.deltaTime;
        CheckOffset();
        CheckDistance();
        transform.Translate(translation, Space.World);
    }

    private void CheckOffset() {
        if (applyOffset) {
            applyOffset = false;
            translation += offset;
            offset = Vector3.zero;
        }
    }

    private void CheckDistance() {
        if (distanceToWaypoint.magnitude < translation.magnitude) {
            translation = distanceToWaypoint;
            applyOffset = true;
            offset = translation - distanceToWaypoint;
            GetNextDirection();
        } else {
            distanceToWaypoint -= translation;
        }
    }
    
    void GetNextDirection() {
        distanceToWaypoint = Waypoints.points[currentWaypoint].position - transform.position;
        moveDirection = distanceToWaypoint.normalized;
        // TODO: Currently we don't care to array out of bounds because we might implement collision detection for enemy destruction before reaching the last waypoint
        ++currentWaypoint;
    }
}