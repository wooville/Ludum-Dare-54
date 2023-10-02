using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenUI : MonoBehaviour
{
    public delegate void StartGameDelegate();
    public static StartGameDelegate startGameDelegate;

    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetButtonDown("Jump"))
            {
                started = true;
                startGameDelegate?.Invoke();
            }
        }
    }
}
