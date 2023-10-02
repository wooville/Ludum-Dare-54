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
    private Player player;

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

    public bool getFlashLightStatus()
    {
        return playerLighting.enabled;
    }

    //Main
    void Start()
    {
        playerLighting = GetComponent<Light2D>();
        setFlashlightOff(playerLighting);
        playerObject = GameObject.FindWithTag("Player");
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        Debug.Log(player.name);
    }

    // Update is called once per frame
    
    void Update()
    {
        if (player.isAlive() == false)
        {
            setFlashlightOff(playerLighting);
        }
        else if (player.getHasLight() == true && Input.GetKey(KeyCode.F) && Time.time > nextFlashlightTime) //change mapping if needed
        {
            toggleFlashLight(playerLighting);
            nextFlashlightTime = Time.time + toggleFlashlightTime;
        }

        
        
        
    
    }
}
