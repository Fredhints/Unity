using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] AudioSource rocketAudio;
    [SerializeField] float force = 200.0f;
    [SerializeField] float rotationSpeed = 200.0f;

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!rocketAudio.isPlaying)
            {
                rocketAudio.Play();
            }
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * force);
        }
        else
        {
            rocketAudio.Stop();
        }
    }


    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))//rotate Left
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))//rotate Right
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotation);
        rb.freezeRotation = false;
    }
}