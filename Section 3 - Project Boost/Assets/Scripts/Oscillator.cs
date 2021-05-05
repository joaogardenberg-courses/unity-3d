using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    Vector3 startingPosition;

    void Start() {
        startingPosition = transform.position;
    }

    void Update() {
        if (period == 0) {
            return;
        }

        float cycles = Time.time / period;
        float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        float movementFactor = (rawSinWave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
