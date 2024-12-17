using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 2.5f;
    private Rigidbody rb;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        GetComponent<Renderer>().material.color = Color.red;
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        transform.parent = GameObject.Find("Entities").transform;
        
        float angle = Random.Range(0.0f, 1.0f) * Mathf.PI * 2;
        float x = Mathf.Cos(angle) * 5;
        float z = Mathf.Sin(angle) * 5;

        transform.position = new Vector3(x, 0, z);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // Move towards target (Object A)
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            rb.velocity = directionToTarget * speed;
        }
    }
    
    public void StartMoveBackCoroutine(float delay)
    {
        StartCoroutine(ResumeMovementAfterDelay(delay));
    }

    IEnumerator ResumeMovementAfterDelay(float delay)
    {
        canMove = false;
        IgnoreCollisionsWithOtherEnemies(true);
        yield return new WaitForSeconds(delay);
        IgnoreCollisionsWithOtherEnemies(false);
        canMove = true;
    }
    
    void IgnoreCollisionsWithOtherEnemies(bool ignore)
    {
        // Find all Object B instances and ignore collisions
        GameObject[] allBs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject otherB in allBs)
        {
            if (otherB != gameObject) // Avoid self-collision check
            {
                Collider myCollider = GetComponent<Collider>();
                Collider otherCollider = otherB.GetComponent<Collider>();

                if (myCollider != null && otherCollider != null)
                {
                    Physics.IgnoreCollision(myCollider, otherCollider, ignore);
                }
            }
        }
    }
    
}
