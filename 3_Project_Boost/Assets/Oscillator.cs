using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    // todo remove from inspector later
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    float period = 2f;

    // 0 for not moved, 1 for fully moved
    [Range(0, 1)] [SerializeField] float movementFactor;
    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: dont allow division by 0
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f; //around 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); //Ranges from -1 to 1
        
        /*This way instead of ranging from -1 to 1, we halve the range and then
        move it from [-0.5, 0.5] to [0, 1]*/
        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
