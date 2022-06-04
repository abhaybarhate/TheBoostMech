using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rocketRb;
    AudioSource audioSource;
    [SerializeField] float rocketThrust = 1000f;
    [SerializeField] float rocketRotationSpeed = 100f;
    [SerializeField] AudioClip mainEngineThrust;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;


    // Start is called before the first frame update
    void Start()
    {
        rocketRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketRotation();
        RocketThrust();
    }

    void RocketThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rocketRb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineThrust);
            }
            if (!mainThrustParticles.isPlaying)
            {
                mainThrustParticles.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainThrustParticles.Stop();
        }
    }

    void RocketRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {

            ApplyRotation(rocketRotationSpeed);
            if (!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {

            ApplyRotation(-rocketRotationSpeed);
            if (!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
        }
        else
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }
    }

    void ApplyRotation(float rotThrust)
    {
        rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotThrust * Time.deltaTime);
        rocketRb.freezeRotation = false;    
    }

}
