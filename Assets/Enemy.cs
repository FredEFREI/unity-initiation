using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate direction toward the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Move the object forward in the direction of the target
        transform.position += direction * speed * Time.deltaTime;

        // Optionally, rotate the object to face the target
        transform.forward = direction;
    }
    
}
