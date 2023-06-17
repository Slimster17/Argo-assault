using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [Header("General Setup Settings")]

    [Header("Movements")]
    [Tooltip("How fast ship moves up and down")][SerializeField] float controlSpeed = 0.1f;
    [Tooltip("Max left and right range of flying")] [SerializeField] float xRange = 5f;
    [Tooltip("Max up and down range of flying")][SerializeField] float yRange = 5f;

    [Header("Screen position based on tuning")]
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = 3f;

    [SerializeField] float controlPitchFactor = -6f;
    [SerializeField] float controlRollFactor = -15f;

    float xThrow, yThrow;

    [Header("Controls")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    [Header("Lasers array")]
    [Tooltip("Add all player lasers here")][SerializeField] GameObject[] lasers;

    

    




    // Start is called before the first frame update
    void Start()
    {
        LaserActivation(false);
    }

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFire();
    }

    

    private void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        //Debug.Log(xThrow + ", " + yThrow);

        float xOffSet = controlSpeed * xThrow;
        float newXPos = transform.localPosition.x + xOffSet;

        float yOffSet = controlSpeed * yThrow;
        float newYPos = transform.localPosition.y + yOffSet;

        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControThrow;
        
        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFire()
    {
        if(fire.ReadValue<float>() > 0.5) 
        {
            LaserActivation(true);
        }
        else 
        {
            LaserActivation(false);
        }
    }


    private void LaserActivation(bool isActive) 
    {
            foreach (var laser in lasers)
            {
                var emissionModule = laser.GetComponent<ParticleSystem>().emission;
                emissionModule.enabled = isActive;
            }
    }
}
