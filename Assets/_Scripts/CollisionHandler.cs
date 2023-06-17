using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelay = 2f;

    [Header("Explosion particles")]
    [SerializeField] ParticleSystem explosionVFX;

    [SerializeField] AudioClip explosionSound;


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.name + "--Collided with--" + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} **Triggered by** {other.gameObject.name}");

        
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        DestroyParticles();
        DisableMovement();
        Invoke("RestartLevel", loadDelay);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;


       
    }


    

    private void RestartLevel()
    {
        int currentlevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentlevel);
    }

    private void DisableMovement() 
    {
        PlayerControls playerControl = GetComponent<PlayerControls>();
        playerControl.enabled = false;
    }

    private void DestroyParticles()
    {
       explosionVFX.Play();
    }
}

