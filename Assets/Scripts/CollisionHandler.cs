using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip sucess;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        crash =
    }
   
    void OnCollisionEnter(Collision other) 
    {
        string tag = other.gameObject.tag;

        if (isTransitioning) {return;}

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
        // todo add particle effect on crash

        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        isTransitioning = true;

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSucessSequence()
    {
        // todo add particle effect on sucess

        audioSource.Stop();
        audioSource.PlayOneShot(sucess);
        isTransitioning = true;

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
