using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delaySeconds = 1.5f;
    [SerializeField] AudioClip landed;
    [SerializeField] AudioClip crashed;
    [SerializeField] ParticleSystem landedParticles;
    [SerializeField] ParticleSystem crashedParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (isTransitioning) return;

        switch(collision.gameObject.tag)
        {
            case "friendly":
                Debug.Log("This thing is friendly");
                break;
            case "finish":
                startNextLevelSequence();
                break;
            case "Fuel":
                Debug.Log("You picked up the Fuel");
                break;
            default:
                startCrashSequence();
                break;
        }

    }

    void startCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        crashedParticles.Play();
        audioSource.PlayOneShot(crashed);
        Invoke("ReloadLevel", delaySeconds);          // Invoke is the function which which delays the given method in the string by the second parameter in the seconds
    }

    void ReloadLevel()
    {
        //We can write like this
        //SceneManager.LoadScene("SampleScene");
        //We can also write in this way
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void startNextLevelSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        landedParticles.Play();
        audioSource.PlayOneShot(landed);
        Invoke("LoadNextLevel", delaySeconds);
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

}
