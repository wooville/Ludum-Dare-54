using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Cave_Darkness : MonoBehaviour
{
    private Light2D playerLighting;
    private GameObject playerObject;
    float SpeedOfLight = 0.05f;
    float SpeedOfDarkness = 0.2f;
    float LightTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerLighting = GetComponent<Light2D>();
        playerObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerObject.transform.position[0]>-90 && playerObject.transform.position[1]<-11) && (playerObject.transform.position[0]<47 && playerObject.transform.position[1]>-105))
        {
            if (playerLighting.intensity > 0.1f && Time.time > LightTimer)
            {
                playerLighting.intensity -= 0.01f;
                LightTimer = Time.time + SpeedOfLight;
                
            }
            else if (playerLighting.intensity == 0f)
            {
                LightTimer = 0;
            }
            Debug.Log("false");
        }
        else
        {
            
            if (playerLighting.intensity <=0.4f && Time.time > LightTimer)
            {
                playerLighting.intensity += 0.001f;
                LightTimer = Time.time + SpeedOfDarkness;
            }
            else if (playerLighting.intensity <= 0.4f)
            {
                LightTimer = 0;
            }
        }
    }
}
