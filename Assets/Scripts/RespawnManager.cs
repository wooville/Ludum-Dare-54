using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private Vector2 respawnPoint;
    public GameObject player;

    void Start()
    {
        // Set the initial respawn point to the player's starting position.
        respawnPoint = player.transform.position;

        Debug.Log("Respawn Point: " + respawnPoint);
    }

    public void SetRespawnPoint(Vector2 checkpointPosition)
    {
        // Set the respawn point to the checkpoint position.
        respawnPoint = checkpointPosition;
    }

    public void RespawnPlayer()
    {
        // Move the player to the respawn point.
        player.transform.position = respawnPoint;
        Debug.Log("Respawning Player!");
    }
}


