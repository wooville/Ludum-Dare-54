using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenUI : MonoBehaviour
{
    public delegate void StartGameDelegate();
    public static StartGameDelegate startGameDelegate;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")){
            startGameDelegate?.Invoke();
        }
    }
}
