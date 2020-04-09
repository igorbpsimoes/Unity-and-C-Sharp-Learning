using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnPlanet : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion startRotation;
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
