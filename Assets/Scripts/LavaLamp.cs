using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LavaLamps : MonoBehaviour
{
    private Light2D globalLight;
    [SerializeField] float startLavaLight = 0.1f;
    [SerializeField] float endLavaLight = 0.1f;
    private bool isDone = false;


    Player player;
    // Start is called before the first frame update
    void Start()
    {
        globalLight = GetComponent<Light2D>();
        globalLight.intensity = startLavaLight;
    }

    // Update is called once per frame
    void Update()
    {
        if(globalLight.intensity < 0.9f && isDone == false)
        {
            globalLight.intensity += 0.0005f;
        }
        else if(isDone == false)
        {
            isDone = true;
        }
        else
        {
            globalLight.intensity -= 0.0005f;
        }

        if(globalLight.intensity < endLavaLight && isDone == true)
        {
            isDone = false;
        }


    }
}
