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

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
        target = GameObject.Find("Target").transform;
        transform.parent = GameObject.Find("Entities").transform;
        
        float angle = Random.Range(0.0f, 1.0f) * Mathf.PI * 2;
        float x = Mathf.Cos(angle) * 5;
        float z = Mathf.Sin(angle) * 5;

        transform.position = new Vector3(x, 0, z);
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
