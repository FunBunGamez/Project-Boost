using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    void OnCollisionEnter(Collision other) 
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Friendly":
                 Debug.Log("You hit a friendly object.");
                 break;

            case "Finish":
                StartSucessSequence();
                break;

            default:
               StartCrashSequence();
                 break;
        }   
    }
    void StartCrashSequence()
    {
        // todo add SFX on crash
        // todo add particle effect on crash
        
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void StartSucessSequence()
    {
        // todo add SFX on crash
        // todo add particle effect on crash

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }
}
