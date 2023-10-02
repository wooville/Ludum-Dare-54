using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Global_lights : MonoBehaviour
{
    private Light2D globalLight;
    private Flashlight flashLight;
    private GameObject playerObject;
    float SpeedOfLight = 0.05f;
    float SpeedOfDarkness = 0.2f;
    float LightTimer = 0f;
    bool inDarkness = false;
    bool isDone = false;
    [SerializeField] float intensityInCave = 0.01f;


    Player player;
    // Start is called before the first frame update
    void Start()
    {
        globalLight = GetComponent<Light2D>();
        playerObject = GameObject.FindWithTag("Player");
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        flashLight = GameObject.FindObjectOfType(typeof(Flashlight)) as Flashlight;
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerObject.transform.position[0]>-90 && playerObject.transform.position[1]<-11) && (playerObject.transform.position[0]<47 && playerObject.transform.position[1]>-105))
        {
            if (globalLight.intensity >= 0f && Time.time > LightTimer && inDarkness == false)
            {
                globalLight.intensity -= 0.01f;
                LightTimer = Time.time + SpeedOfLight;

            } 
            else if(globalLight.intensity <intensityInCave && player.getHasLight() == true && flashLight.getFlashLightStatus() == true && isDone == true)
            {
                globalLight.intensity = intensityInCave;
                inDarkness = true;
            }
            else if (globalLight.intensity <= 0f)
            {
                LightTimer = 0;
                inDarkness = true;
                isDone = true;
            }
            else if (flashLight.getFlashLightStatus() == false && isDone == true)
            {
                globalLight.intensity = 0f;
                inDarkness = true;
            }
        }
        else
        {
            
            if (globalLight.intensity <=0.4f && Time.time > LightTimer)
            {
                globalLight.intensity += 0.01f;
                LightTimer = Time.time + SpeedOfDarkness;
                inDarkness = false;
                isDone = false;
            }
            else if (globalLight.intensity >= 0.4f)
            {
                LightTimer = 0;
                
            }
        }
    }
}
