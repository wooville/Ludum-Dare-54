using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    public Transform target;
    private Light2D playerLighting;
    float toggleFlashlightTime = 0.2f;
    float nextFlashlightTime = 0.0f;

    private GameObject playerObject;

    //Function calls
    private void setFlashlightOff(Light2D flashlight)
    {
        // Sets Flashlight off
        flashlight.enabled = false;
    }
    private void toggleFlashLight(Light2D flashlight)
    {
        flashlight.enabled = (!flashlight.enabled);
    }

    //Main
    void Start()
    {
        playerLighting = GetComponent<Light2D>();
        setFlashlightOff(playerLighting);
        playerObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && Time.time > nextFlashlightTime) //change mapping if needed
        {
            //Needs to be another if statement
            toggleFlashLight(playerLighting);
            nextFlashlightTime = Time.time + toggleFlashlightTime;

        }
        //playerLighting.transform.rotation = Quaternion.LookRotation(realativePos);
    
    }
}
