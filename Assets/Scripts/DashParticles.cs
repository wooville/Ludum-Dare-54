using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticles : MonoBehaviour
{
    // Start is called before the first frame update

    Player player;
    TrailRenderer trail;
    private GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        trail = GetComponent<TrailRenderer>();
        trail.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isGrounded() == true || player.isAlive() == false)
        {
            trail.emitting = false;
        }
        else if (player.isDashing() == true)
        {
            trail.emitting = true;
        } 
        
    }
}
