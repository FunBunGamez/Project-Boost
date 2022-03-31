using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem boosterParticles;
    [SerializeField] ParticleSystem rightParticles;
    [SerializeField] ParticleSystem leftParticles;

    // CACHE - e.g. references for readability or speed
    Rigidbody rb;
    AudioSource audioSource;

     // STATE - private instance (member) variables
     // bool isAlive; - e.g.
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            } 
            if (!boosterParticles.isPlaying)
            {
                boosterParticles.Play();
            }            
        }
        else
            {
                audioSource.Stop();
                boosterParticles.Stop();
            }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!rightParticles.isPlaying)
            {
                rightParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if (!leftParticles.isPlaying)
            {
                leftParticles.Play();
            }
        }
        else
        {
            rightParticles.Stop();
            leftParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so physics can take over
    }
}