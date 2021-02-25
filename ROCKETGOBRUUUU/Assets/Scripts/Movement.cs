using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS ------------------------------------------------
    // CACHE -----------------------------------------------------
    // STATE -----------------------------------------------------

    [SerializeField] float force = 200.0f;
    [SerializeField] float rotationSpeed = 200.0f;
    [SerializeField] AudioClip mainSFX;

    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;

    Rigidbody rb;
    AudioSource rocketAudio;

    void Start()
    {
        rocketAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))//rotate Left
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))//rotate Right
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }


    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }


    private void StopThrusting()
    {
        rocketAudio.Stop();
        mainParticles.Stop();
    }


    private void StartThrusting()
    {
        if (!mainParticles.isPlaying) mainParticles.Play();

        if (!rocketAudio.isPlaying) rocketAudio.PlayOneShot(mainSFX);

        rb.AddRelativeForce(Vector3.up * Time.deltaTime * force);
    }


    private void StopRotating()
    {
        rightParticles.Stop();
        leftParticles.Stop();
    }


    private void RotateRight()
    {
        if (!leftParticles.isPlaying) leftParticles.Play();

        ApplyRotation(-rotationSpeed);
    }


    private void RotateLeft()
    {
        if (!rightParticles.isPlaying) rightParticles.Play();
        
        ApplyRotation(rotationSpeed);
    }


    private void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotation);
        rb.freezeRotation = false;
    }
}