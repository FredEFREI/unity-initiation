using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float repulseForce = 10f;    
    public float repulseDuration = 0.5f; 
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Rigidbody rbB = col.gameObject.GetComponent<Rigidbody>();
            
            if (rbB != null)
            {
                // Calculate repulsion direction
                Vector3 repulsionDirection = (col.gameObject.transform.position - transform.position).normalized;
                rbB.velocity = Vector3.zero; // Stop current movement
                rbB.AddForce(repulsionDirection * repulseForce, ForceMode.Impulse);

                // Notify Object B to resume moving toward A after repulsion
                col.gameObject.GetComponent<Enemy>()?.StartMoveBackCoroutine(repulseDuration);
            }
        }
    }
}
