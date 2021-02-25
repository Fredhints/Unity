using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float Delay = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip successSFX;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    AudioSource rocketAudio;

    bool isTransitioning = false;

    bool collisionDisabled= false;

    void Start()
    {
        rocketAudio = GetComponent<AudioSource>();
    }



    private void Update()
    {
        RespondToDebugKeys();
    }


    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) LoadNextLevel();
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;//toggle collision
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("hit friedly collision");
                break;
            case "Finish":
                StartLoadNextLevelSequence();
                Debug.Log("Level Finished");
                break;
            case "Fuel":
                Debug.Log("You pick up Fuel");
                break;
            default:
                StartCrashSequence();
                Debug.Log("sorry you blew up!");
                break;
        }
    }


    void StartCrashSequence()
    {

        GetComponent<Movement>().enabled = false;
        
        isTransitioning = true;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(deathSFX);

        deathParticles.Play();

        Invoke("ReloadLevel", Delay);
    }


    void StartLoadNextLevelSequence()
    {
       
        GetComponent<Movement>().enabled = false;

        successParticles.Play();
        isTransitioning = true;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(successSFX);

        Invoke("LoadNextLevel", Delay);
    }


    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
    }


    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}

