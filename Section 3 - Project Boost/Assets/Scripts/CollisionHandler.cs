using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    Movement movement;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision other) {
        if (isTransitioning || collisionDisabled) {
            return;
        }

        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("This thing is friendly :)");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence() {
        isTransitioning = true;
        movement.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence() {
        isTransitioning = true;
        movement.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings) {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void RespondToDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
        }
    }
}
