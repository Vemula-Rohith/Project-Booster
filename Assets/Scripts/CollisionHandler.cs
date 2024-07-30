using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    GameObject Fuel;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip finish;

    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisable;

    public Health health;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        Cheatcodes();
    }

    void Cheatcodes()
        {
            if(Input.GetKey(KeyCode.L))
            {
                FinishLevel();
            }
            else if(Input.GetKey(KeyCode.C))
            {
                collisionDisable = !collisionDisable;
            }
        }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisable)
        {
            return;
        }
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Start The Game");
                break;
            case "Finish":
                SuccessSequence();
                break;
            default:
                CrashSequence();
                break;
        }
    }
        void SuccessSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(finish);
            finishParticles.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("FinishLevel",2f);
        }
        void CrashSequence()
        {

            audioSource.Stop();
            isTransitioning = true;
            audioSource.PlayOneShot(explosion);
            explosionParticles.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel",2f);
        }
        void ReloadLevel()
        {
            int level = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(level);
        }
        void FinishLevel()
        {
            int level = SceneManager.GetActiveScene().buildIndex;
            int nextLevel = level + 1;
            if(nextLevel == SceneManager.sceneCountInBuildSettings)
            {
                nextLevel = 0;
            }
            SceneManager.LoadScene(nextLevel);
        }
}