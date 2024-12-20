using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomShotBouncing : RandomShot
{
    
    public float speed = 4;  //Bullet travel speed;
    public int damage = 4; //Flat damage
    public float attackSpeed = 6f; //Seconds between each shot
    public float range = 25f;  //Range before destruction;
    private  Vector3 startPosition;
    public Health entityHealth;
    private Vector3 direction;
    public int numberOfCollision = 3;

    
    // Start is called before the first frame update
    void Start()
    {
        float angle = Random.Range(0.0f, 1.0f) * Mathf.PI * 2;
        direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction  * speed * Time.deltaTime;

        if(Mathf.Abs(Vector3.Distance(startPosition, this.transform.position)) > range){
            Destroy(this.gameObject);
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Enemy") || (collision.gameObject.tag == "Player") || (collision.gameObject.tag == "Wall"))
        {
            direction = new Vector3(direction.x*(-1), 0, direction.z*(-1));
        }
        
        if (entityHealth == null)
        {
            entityHealth = collision.gameObject.GetComponent<Health>();
            if (entityHealth == null)
                return;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            numberOfCollision--;
            if (numberOfCollision == 0)
            {
                Destroy(gameObject);
            }
            entityHealth.takeDamage(damage);
        }
    }
}
