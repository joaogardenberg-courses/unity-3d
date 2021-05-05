using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] float mainThrust = 1250f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    Rigidbody rigidBody;
    AudioSource audioSource;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            StartThrusting();
        } else if (audioSource.isPlaying) {
            StopThrusting();
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A)) {
            RotateLeft();
        } else {
            StopRotateLeft();
        }

        if (Input.GetKey(KeyCode.D)) {
            RotateRight();
        } else {
            StopRotateRight();
        }
    }

    void StartThrusting() {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngineSFX);
        }

        if (!mainEngineParticles.isPlaying) {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting() {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void RotateRight() {
        ApplyRotation(-rotationThrust);

        if (!leftThrusterParticles.isPlaying) {
            leftThrusterParticles.Play();
        }
    }

    void StopRotateRight() {
        leftThrusterParticles.Stop();
    }

    void RotateLeft() {
        ApplyRotation(rotationThrust);

        if (!rightThrusterParticles.isPlaying) {
            rightThrusterParticles.Play();
        }
    }

    void StopRotateLeft() {
        rightThrusterParticles.Stop();
    }

    void ApplyRotation(float rotation) {
        GetComponent<Rigidbody>().freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        GetComponent<Rigidbody>().freezeRotation = false;
    }
}
