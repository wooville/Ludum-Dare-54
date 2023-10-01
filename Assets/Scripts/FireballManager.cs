using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform[] launchPoints;
    public float fireballInterval = 2f;

    private void Start()
    {
        // Start spawning fireballs at regular intervals.
        InvokeRepeating("SpawnFireball", 0f, fireballInterval);
    }

    private void SpawnFireball()
    {
        foreach (Transform launchPoint in launchPoints)
        {
            // Instantiate a new fireball at the launch point.
            Instantiate(fireballPrefab, launchPoint.position, Quaternion.identity);
        }
        
    }
}
