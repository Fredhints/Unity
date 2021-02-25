using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float Delay = 1f;


    void OnCollisionEnter(Collision collision)
    {
    
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
        Invoke("ReloadLevel", Delay);
    }

    void StartLoadNextLevelSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", Delay);
    }


    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

