using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follower : MonoBehaviour
{
    public float coordidates_X;
    public float coordidates_Y;
    public float coordidates_Z;
    public Transform playerTransform;
    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        offset.x = coordidates_X;
        offset.y = coordidates_Y;
        offset.z = coordidates_Z;
        if (playerTransform != null)
        {
            // Set the follower object's position to match the player's position
            transform.position = playerTransform.position+offset;
        }
        else
        {
            Debug.LogWarning("Player Transform is not assigned.");
        }
    }
}

