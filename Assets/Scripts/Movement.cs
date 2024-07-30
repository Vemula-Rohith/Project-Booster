using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftSideBooster;
    [SerializeField] ParticleSystem rightSideBooster;

    Rigidbody rb;
    AudioSource audioSource;
    Scene scene;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        scene = SceneManager.GetActiveScene();
        if(scene.name == "leftBooster")
        {
            Debug.Log("Only you're left booster works");
        }
        if(scene.name == "switchControls")
        {
            Debug.Log("You're default 'left right' keys controls are swapped with each other");
        }
        Console.Clear();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if(scene.name == "leftBooster")
        {
            mainThrust = 1500f;
            if(Input.GetKey(KeyCode.D))
            {
                RotateRight();
            }
            else
            {
                StopRotation();
            }
        }
        else if(scene.name == "switchControls")
        {
            if(Input.GetKey(KeyCode.D))
            {
                RotateLeft();
            }
            else if(Input.GetKey(KeyCode.A))
            {
                RotateRight();
            }
            else
            {
                StopRotation();
            }
        }
        else
        {
            if(Input.GetKey(KeyCode.A))
            {
                RotateLeft();
            }
            else if(Input.GetKey(KeyCode.D))
            {
                RotateRight();
            }
            else
            {
                StopRotation();
            }
        }
    }

    private void StopThrusting()
    {
        mainBooster.Stop();
        audioSource.Stop();
    }

    public void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void StopRotation()
    {
        if (rightSideBooster || leftSideBooster)
        {
            rightSideBooster.Stop();
            leftSideBooster.Stop();
        }
    }

    private void RotateRight()
    {
        if (!leftSideBooster.isPlaying)
        {
            leftSideBooster.Play();
        }
        ApplyRotation(-rotationThrust);
    }

    private void RotateLeft()
    {
        if (!rightSideBooster.isPlaying)
        {
            rightSideBooster.Play();

        }
        ApplyRotation(rotationThrust);
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}